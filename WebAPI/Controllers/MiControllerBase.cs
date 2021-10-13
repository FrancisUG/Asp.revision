using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    //Tendra una ruta generica para todos los controllers
    [Route("api/[controller]")]

    //Indicamos se trata d un componente apicontroller
    [ApiController]


    //Indicamos q hereda dl padre d todos los controllers..ControllerBase
    public class MiControllerBase : ControllerBase
    {
        

        private IMediator _mediator;


       
       protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    

        
        
    }
}