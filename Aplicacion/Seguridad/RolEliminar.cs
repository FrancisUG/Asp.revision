using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RolEliminar
    {
        public class Ejecuta : IRequest {
            
            public string Nombre {get;set;}
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>{

            public EjecutaValida (){

                RuleFor(x => x.Nombre).NotEmpty();
            }
            
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly RoleManager<IdentityRole> _roleManger;

            public Manejador(RoleManager<IdentityRole> roleManager){
                _roleManger = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               //Nos aseguramos q el rol exista antes de eliminarlo
                var role = await _roleManger.FindByNameAsync(request.Nombre);

                if(role == null){
                    
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "No existe el rol"});
                }

                //Sino se cumple el nulo seguimos con la sgte. linea 
                //Si es q se encontro el obj rol Eliminalo
                var resultado = await _roleManger.DeleteAsync(role);

                if(resultado.Succeeded){

                    //Si todo es OK retorname un FLat d conformacion
                    return Unit.Value;
                }

                throw new System.Exception("No se pudo eliminar el rol");


            }
        }
    }
}