using System.Collections.Generic;
using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
            //Creamos un metodo e indicamos q reciba 2 param, Usuario-usuario q viene dsd Dominio
         string CrearToken (Usuario usuario, List<string> roles);
    }
}