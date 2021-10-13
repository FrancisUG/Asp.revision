using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        

        [HttpPost("login")]

        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {

            //Aqui invoca a la clase Login y al "Manejador" contenidas en mi proyecto "Aplicacion" Login invoca toda esa logica  
            return await Mediator.Send(parametros);
        }


        [HttpPost("registrar")]

        //Metodo asincrono q me devuelve un UsuarioData, los param q ingresan son desde la clase "Registrar" y Ejecuta q es quien tiene los param 
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {

            //Paso los parametros q el usuario me va enviar
            return await Mediator.Send(parametros);


        }


        //http://localhost:5000/api/Usuario  "el Endpoint "

        [HttpGet]

        //No tiene param d entrada
        public async Task<ActionResult<UsuarioData>> DevolverUsuario()
        {

            //Le indicamos q me devuelva a Ejecutar

            return await Mediator.Send(new UsuarioActual.Ejecutar());
        }

        [HttpPut]

        public async Task<ActionResult<UsuarioData>> Actualizar(UsuarioActualizar.Ejecuta parametros)
        {

            return await Mediator.Send(parametros);
        }

    }
}