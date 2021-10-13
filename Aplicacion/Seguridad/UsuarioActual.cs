using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {

       
        public class Ejecutar: IRequest<UsuarioData>{}

       
        public class Manejador : IRequestHandler <Ejecutar, UsuarioData>
        {

            
            private readonly UserManager<Usuario> _userManager;

            private readonly IJwtGenerador _jwtGenerador;

            private readonly IUsuarioSesion _usuarioSesion;


            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion){

                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;

            }
            //Para poder trabajar el "UsuarioData" necesitamos 3 clases q inyectar dentro dl manejador
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {

                //_userManager busca un usuario en la Db y lo va devolver mediante "usuario"
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

                 var resultadoRoles = await _userManager.GetRolesAsync(usuario);

                //Convertimos la lista de errores a List
                var listaRoles = new List<string>(resultadoRoles);

                return new UsuarioData{

                    NombreCompleto = usuario.NombreCompleto,
                    Username = usuario.UserName,
                     
                    //Nos pide q le pasemos 1 param y le pasamos "usuario"
                    Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                    Imagen = null,
                    Email = usuario.Email
                };
            }
        }
    }
}