using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{

    //La cabecera
    public class Editar
    {
        public class Ejecuta : IRequest
        {

            //Param q ingresan dsd el cliente 
            public Guid InstructorId { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Titulo { get; set; }

        }

        //Clase q valida la data q va ingresar
        public class EjecutaValida : AbstractValidator<Ejecuta>
        {

            public EjecutaValida()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Titulo).NotEmpty();

            }

        }


        //Clase q maneja la logica e implementa la interface
        public class Manejador : IRequestHandler<Ejecuta>
        {
            //Llamamos al _instructorRepositorio q es la clave
            private readonly IInstructor _instructorRepositorio;


            //Lo inyectamos dentro dl constructor, osea se crea el obj
            public Manejador(IInstructor instructorRepositorio)
            {

                _instructorRepositorio = instructorRepositorio;
            }


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hacemos q _instructorRepositorio me ejecute el metodo "Actualiza" q pide los sgntes param
                //Este valor devolvera un entero indicando cuantas transacciones se han realizado
                var resultado = await _instructorRepositorio.Actualiza(request.InstructorId, request.Nombre, request.Apellidos, request.Titulo);

                //Si fue posible la transaccion, retorname un "Unit.Value"
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo actualizar la data del instructor");
            }
            //Todo esto es la logica para llamar al repositorio dl SP
        }
    }
}