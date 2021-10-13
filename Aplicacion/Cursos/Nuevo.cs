using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public List<Guid> ListaInstructor{get;set;}


            //Add estos 2 attrib para el manejo d precio. 
            public decimal Precio {get;set;}

            public decimal Promocion {get;set;}

           

            

        }
            public class EjecutaValidacion : AbstractValidator<Ejecuta>{
                public EjecutaValidacion(){
                    RuleFor(x => x.Titulo).NotEmpty();
                    RuleFor(x => x.Descripcion).NotEmpty();
                    RuleFor(x => x.FechaPublicacion).NotEmpty();
                }
            }

        //Clase de la logica d la transaccion 
        //Se indica de IRequestHandler va a trabajar usando la definicion de datos de "Ejecuta"
        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CursosOnlineContext _context;

          
            public Manejador(CursosOnlineContext context)
            {
                //La relacion de dependencia, --> El parámetro context se inyecta
                _context = context;

            }

           
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid _cursoId = Guid.NewGuid();
                var curso = new Curso
                {
                    //CursoId proviene d un Guid
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                    //Venimos a la clase Manejador se creara auto.. la fecha, le da la fecha actual al cliente
                    //--> De aqui nos vamos a Cursos/Editar 
                };
               
                _context.Curso.Add(curso);

              
                if(request.ListaInstructor!=null){
                    

                   
                    foreach(var id in request.ListaInstructor){

                        var cursoInstructor = new CursoInstructor{
                            CursoId = _cursoId,
                            InstructorId = id
                        };

                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }


                //LOGICA PARA INSERTAR UN PRECIO DEL CURSO
                var precioEntidad = new Precio {

                    //Primero le pasamos un _cursoId
                    CursoId = _cursoId,

                    //Le pasamos el precio vamos asignar q viene d request
                    PrecioActual= request.Precio,

                    Promocion = request.Promocion,

                    //Creamos el id del precio q tenemos q crear, #aleatorio, unico
                    PrecioId = Guid.NewGuid()
                };
                   
                _context.Precio.Add(precioEntidad);

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                //Else lanza el error 
                throw new Exception("No se pudo insertar el curso");

            }
        }
    }
}