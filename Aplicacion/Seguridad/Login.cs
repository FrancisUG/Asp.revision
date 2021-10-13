using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
       
        public class Ejecuta : IRequest<UsuarioData>
        {

            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {

            public EjecutaValidacion()
            {

                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                
            }

        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;

            //var q representa al token 
            private readonly IJwtGenerador _jwtGenerador;


          
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {

                //INYECTAMOS LAS VARIABLES y las convertimos en objs
               _userManager  = userManager;
               _signInManager  = signInManager;
               _jwtGenerador = jwtGenerador;

            }


            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    //error d tipo NO AUTORIZADO
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }
               

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                //Sacamos la lista de errores, recordar le pasamos 'usuario' xq es lo q se crea 

                var resultadoRoles = await _userManager.GetRolesAsync(usuario);

                //Convertimos la lista de errores a List
                var listaRoles = new List<string>(resultadoRoles);

                if (resultado.Succeeded)
                {
                  
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                        Username = usuario.UserName, 
                        Email = usuario.Email,
                        Imagen = null
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);

               


               

            }
        }
    }
}