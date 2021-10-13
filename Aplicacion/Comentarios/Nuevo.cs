using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest{

            public string Alumno {get;set;}
            public int Puntaje {get;set;}
            public string Comentario {get;set;}
            public Guid CursoId{get;set;}
            
            
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>{

            public EjecutaValidacion(){
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.Comentario).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();
            }


        }

        //Le pasamos el param q va ejecutar e implementamos la interface
        public class Manejador : IRequestHandler<Ejecuta>
        {
            //Hacemos la referencia de EnttyF de Persistencia
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context){

                _context = context;

            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Creamos el obj Comentario, importamos la entidad Comentario dsd Dominio, posterior add los valores q se van insertar
                var comentario = new Comentario{

                    ComentarioId = Guid.NewGuid(),
                    Puntaje = request.Puntaje,
                    Alumno = request.Alumno,
                    ComentarioTexto = request.Comentario,
                    CursoId = request.CursoId,
                    FechaCreacion = DateTime.UtcNow

                };

                //Agg este obj comentario dentro d la entidad "Comentario"
                _context.Comentario.Add(comentario);

               var resultados = await  _context.SaveChangesAsync();

               if(resultados > 0){

                   return Unit.Value;

               }
               throw new Exception ("No se pudo insertar el comentario");

            }
        }
    }
}