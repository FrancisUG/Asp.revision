using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    
    public class Usuario : IdentityUser
    {
        public string NombreCompleto {get;set;}
        
        
    }
}
//Creamos una clase dentro de mi proyecto Dominio q represent el user de mi APP llamada Usuario


