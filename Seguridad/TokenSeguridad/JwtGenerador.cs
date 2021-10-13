using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad
{
    //Le indicamos q implemente los metodos q estan en la interface IJwtGenerador
    public class JwtGenerador : IJwtGenerador
    {
        //Le pasamos ahora la lista d roles dentro del TOKEN
        public string  CrearToken(Usuario usuario, List<string> roles)
        {
           

            var claims = new List<Claim>{

                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)

            };

            //Verificamos q ese array d roles no sea nulo, este param List<string> roles lo convertimos en claim 
            
            if(roles != null){

                //Bucle q crea por cada elemento d la lista un claim
                foreach(var rol in roles){
                    
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }


        
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));

           

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            
            var tokenDescripcion = new SecurityTokenDescriptor{

                
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales
            };

           
            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);


        }

       
    }
}


