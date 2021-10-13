using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI.Controllers
{
    //  Anotacón APIController -->>Endpoint http://localhost:5000/WeatherForecast
    [ApiController]
    [Route("[controller]")] //[controller] significa q será remplazado por el nombre de la CLASE. 

    //Clase de tipo CONTROLLER
    public class WeatherForecastController : ControllerBase //Hereda dsd la clase Controller base de Asp.NetCore
    {
        
        private readonly CursosOnlineContext context;



        public WeatherForecastController(CursosOnlineContext _context){
            this.context = _context;

        }

       [HttpGet] //Cuando el cliente quiera consumir, habrá un pedido tipo GET


     
       public IEnumerable<Curso> Get(){
           //Me va a devolver un array de nombres 
           return context.Curso.ToList();
       }
    }
}
