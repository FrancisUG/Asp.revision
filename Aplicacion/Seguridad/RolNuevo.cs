using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RolNuevo
    {
        public class Ejecuta : IRequest
        {

            //Entra 1 param d tipo nombre
            public string Nombre { get; set; }

        }

        public class ValidaEjecuta : AbstractValidator<Ejecuta>
        {

            public ValidaEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            //Obj q vamos inyectar es d tipo Rol manager y proviene dl IdentityRole
            public Manejador(RoleManager<IdentityRole> roleManager)
            {

                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Para poder buscar un rol
                var role = await _roleManager.FindByNameAsync(request.Nombre);

                //Si el rol ya existe y diferent d null
                if (role != null)
                {

                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya existe el rol" });
                }

                //Sino encuentra un rol LO CREA
                var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el rol");
            }
        }
    }
}