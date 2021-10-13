using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>> {

            
        }

        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            //Llamamos a la representacion d los obj d la DB
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context){
                _context = context;
            }
            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Traemos los obj d la Db y los convertimos en una lista
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }
    }
}