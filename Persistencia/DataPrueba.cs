using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        
        
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
        {

            
            if (!usuarioManager.Users.Any())
            {
                //Creamos un obj para insertar en nuestra Db
                //UserName & Email heredan desde la clase "IdentityUser"
                var usuario = new Usuario { NombreCompleto = "Francis Cb", UserName = "francis", Email = "francis94@gmail.com" };

                
                await usuarioManager.CreateAsync(usuario, "Password123$");


            }

        }
           
    }
}
//alt-shift+ F para identar codigo.