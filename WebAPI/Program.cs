using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI
{
    public class Program
    {

            // TODAS NUESTRAS EJECUCIONES  <--------------------------
       
        public static void Main(string[] args)
        {
            //------>>>> EDITAMOS y creamos una var q almacene un hostserver ADEMAS DE MIGRACIONES <<<<<-------
            var hostserver = CreateHostBuilder(args).Build();
            //Creamos un contexto, ambiente
            using (var ambiente = hostserver.Services.CreateScope())
            {

                //Indicamos q el ambiente services = 
                var services = ambiente.ServiceProvider;
                try
                {
                    
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();


                    //<<<<<------- A ----->>>>>>>>
                    var context = services.GetRequiredService<CursosOnlineContext>();

                   
                    context.Database.Migrate();


                    
                    DataPrueba.InsertarData(context, userManager).Wait();

                    

                }catch(Exception e){
                    //la var loggin intercepta la interfaz Ilogger q se esjecuta dentro d la clase Program
                    var logginn = services.GetRequiredService<ILogger<Program>>();

                    //Le inidico si ocurre un error imprimelo  con detalle
                    logginn.LogError(e, "Ocurrió un error en la migración");
                }

            }
            //Para correr la sentencia
            hostserver.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

 