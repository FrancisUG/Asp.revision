using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
       
        public class Ejecuta : IRequest <UsuarioData>{
            public string NombreCompleto {get;set;}
            public string Email {get;set;}
            public string Password {get;set;}
            public string Username{get;set;}
            
        }

       

        public class EjecutaValidador : AbstractValidator<Ejecuta>{

            public EjecutaValidador(){

                //Utilizamos la palabra reservada RuleFor
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();

            }

        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

           
            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador){

                //Inyectamos dejamos listos los objs para trabajar, osea convertimos nuestros param en ojbs
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;

            }

           
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
              
              


               var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();

               if(existe){

                   //Si existe, arrojame un codigo d error... osea el "HttpStatusCode.BadRequest"
                   throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Existe ya un usuario con este email"});
               }

               var existeUserName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
               if(existeUserName){
                   throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Existe ya un usuario con este username"});
                }
               






               //Creamos el obj q ingresara a la DB y le damos los valores Json q va a tener

               var usuario = new Usuario {

                   NombreCompleto =request.NombreCompleto,
                   Email = request.Email,
                   UserName = request.Username

               };

              var resultado = await _userManager.CreateAsync(usuario, request.Password);

              if(resultado.Succeeded)
              return new UsuarioData{
                  NombreCompleto = usuario.NombreCompleto,
                  Token = _jwtGenerador.CrearToken(usuario, null),
                  Username = usuario.UserName,
                  Email = usuario.Email
              };

              throw new Exception("No se pudo agregar al nuevo usuario");

             
            }
        }
    }
}