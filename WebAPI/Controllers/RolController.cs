using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //ControllerBase tiene el AutoMapper listo para ser implementado
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]

        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta parametros){
            
            return await Mediator.Send(parametros);
        }

        [HttpDelete("eliminar")]

         public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta parametros){

             return await Mediator.Send(parametros);

         }

         [HttpGet("lista")]

         public async Task<ActionResult<List<IdentityRole>>> Lista(){
             return await Mediator.Send(new RolLista.Ejecuta());
         }

          [HttpPost("agregarRoleUsuario")]

          public async Task<ActionResult<Unit>> AgregarRoleUsuario(UsuarioRolAgregar.Ejecuta parametros){

              return await Mediator.Send(parametros);
          }

           [HttpPost("eliminarRolUsuario")]

           public async Task<ActionResult<Unit>> EliminarRoleUsuario(UsuarioRolEliminar.Ejecuta parametros){

               return await Mediator.Send(parametros);
           }



            [HttpGet("{username}")]

            public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuario(string username){

                        //'UserName' sera = al valor q ingresa el cliente externo 'username'
                return await Mediator.Send(new ObtenerRolesPorUsuario.Ejecuta{UserName = username});
            }
    }
}