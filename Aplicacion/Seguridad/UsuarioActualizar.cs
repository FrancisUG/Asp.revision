using System.Collections.Generic;
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
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {

            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            //No se actualiza el Username pero es necesario q pase ese param
            public string Username { get; set; }



        }

        //Validamos

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {

            public EjecutaValidador()
            {

                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }


        //------->>>>>>     Clase q implementa la logica     <<<<<<<----------------------

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            //Llamamos al _userManager dl coreIdentty, a 'contex' del CursosOnlineContext
            // y el obj q permita crear el token para poder actualizar la data

            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

            //Anadimos el param para la clave hash
            private readonly IPasswordHasher<Usuario> _passwordHasher;

            //Contrc
            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IPasswordHasher<Usuario> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordHasher = passwordHasher;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Evaluamos q el usuario exista, lo byscamos a ver si existe
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);

                if (usuarioIden == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con este username" });
                }

                //Q pasa si le mandamos un email q ya existe....? VALIDAMOS 
                //Llamamos a _context y la entidad User q contiene todos los usuarios

                //Valida si YA EXISTE ALGUN USUARIO Q tenga el email q queremos actualizar, si existe ese email tomado po alguien mas 
                //SO lanza el error q diga ese email ya pertenece a otro usuario

                var resultado = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();

                //Si es q existe ese usuario lanza el error 
                if (resultado)
                {

                    throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Este email pertenece a otro usuario" });

                }

                //ELSE continuamos con la logica
                usuarioIden.NombreCompleto = request.NombreCompleto;

                // llamamos al _passwordHasher.HashPassword q es el metodo q encripta la password, pide 2 param le pasamos usuarioIden y el request.password

                usuarioIden.PasswordHash = _passwordHasher.HashPassword(usuarioIden, request.Password);

                //Ademas actualizamos el email
                usuarioIden.Email = request.Email;

                //Posterior llamamos al _userManager para indicar q vamos actualizar la data
                var resultadoUpdate = await _userManager.UpdateAsync(usuarioIden);

                //Para obtener la lista d errores d este usuario hacemos esto.. A GetRolesAsync le pasamos como param (usuarioIden) y retorna un 'resultadoRoles'
                //pero como lo q devuelve es un 'Ilist' POSTERIOR CASTEAMOS para q sea el retorno d tipo lista
                var resultadoRoles = await _userManager.GetRolesAsync(usuarioIden);
                var listRoles = new List<string>(resultadoRoles);


                //Si resultadoUpdate fue exitoso devuelv la nueva data dl usuario registrado
                if (resultadoUpdate.Succeeded)
                {

                    //Devuelve un obj de tipo UsuarioData y dentro d las llaves la data q va devolver
                    return new UsuarioData
                    {
                        NombreCompleto = usuarioIden.NombreCompleto,
                        Username = usuarioIden.UserName,
                        Email = usuarioIden.Email,

                        //Indicamos q tambien devuelva el token, CrearToken tiene 2 param el usuario y la lista de roles d ese usuario, como no tengo la lista de roles d ese
                        //usuario hacmos la linea var resultadoRoles = await _userManager.GetRolesAsync(usuarioIden); y posterior la lista d roles d ese usuario
                        Token = _jwtGenerador.CrearToken(usuarioIden, listRoles)

                    };
                }
                throw new System.Exception("No se pudo actualizar el usuario");


            }



        }


    }
}