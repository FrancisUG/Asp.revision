using System;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        //Valores para inyectar
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;
        //Tendrá 2 param "next" y "logger" para gestionar los estados d respuestas hacia el cliente
        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {

            _next = next;
            _logger = logger;
        }     
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //Para q pase ese evnt dsd Controller hacia "Aplicacion"
                await _next(context);
            }
            catch (Exception ex)
            {
                //Pasamos el context, la excepcion y el _logger
                await ManejadorExcepcionAsincrono(context, ex, _logger);
            }

        }


        private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
        {

            //Declaramos como objs los errores q a futuro se pueden dar en la validacion
            object errores = null;

            //Configuramos el tipo de error q se ha creado

            switch (ex)
            {
     
                case ManejadorExcepcion me:

                    //Indicamos q refleje el detalle dl error y q tipo 
                    logger.LogError(ex, "Manejador Error");

                
                    errores = me.Errores;

                    //Y tambien el codigo d estatus q le voy a mandar al cliente
                    context.Response.StatusCode = (int)me.Codigo;
                    break;

               
                case Exception e:
                    logger.LogError(ex, "Error de servidor");

                
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            
            context.Response.ContentType = "Application/json";

            //Validamos si tengo errores o no si tiene algún valor
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });

                await context.Response.WriteAsync(resultados);
            }


        }
    }
}