using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest {

            public Guid Id {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context){

                _context = context;

            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                    var instructoresDB = _context.CursoInstructor.Where(x =>x.CursoId == request.Id);
                    
                    foreach(var instructor in instructoresDB){

                        _context.Remove(instructor);
                    }

                    var comentariosDB = _context.Comentario.Where(x => x.CursoId == request.Id);
                    foreach(var cmt in comentariosDB){
                        _context.Comentario.Remove(cmt);
                    }


                    var precioDb = _context.Precio.Where(x =>x.CursoId == request.Id).FirstOrDefault();
                    if(precioDb !=null){
                        _context.Precio.Remove(precioDb);
                    }



                //Le digo q me busque el curso q voy a eliminar primero.
                var curso = await _context.Curso.FindAsync(request.Id);

                //Si el curso es null NO EXISTE
                if(curso==null){

                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "No se encontró el curso"}); 
                }
                
                _context.Remove(curso);

                //Hacemos la confirmacion 
               var resultado = await _context.SaveChangesAsync();

               if(resultado>0){
                   return Unit.Value;
               }

               throw new Exception("No se pudieron guradar los cambios");


            }
        }
    }
}