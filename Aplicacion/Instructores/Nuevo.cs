using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            // public Guid InstructorId {get;set;} no es necesario xq ya se crea dentro d la logica
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Titulo { get; set; }

        }

        //Creamos otra clase q valide esos param 
        //Le indicamos q va a validar a "Ejecuta"
        public class EjecutaValida : AbstractValidator<Ejecuta>
        {
            public EjecutaValida()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Titulo).NotEmpty();

            }

        }

        //Ahora creamos una clase para incorporar la logica de la transaccion

        public class Manejador : IRequestHandler<Ejecuta>
        {
            //Implementamos la inyeccion de "Persistencia"
            private readonly IInstructor _instructorRepository;

            //Utilizamos el constrtr d la clase "Manejador" para poder crear este obj
            public Manejador(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;

            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Ahora ejecutamos la operacion, pasan 1 a 1 los param 
                //Lo q retorna es un valor d tipo Nuevo, de tipo Task y recordar q es una transaccion asincrona
                var resultado = await _instructorRepository.Nuevo(request.Nombre, request.Apellidos, request.Titulo);

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el Instructor");
            }
        }
    }
}