using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class ConsultaId
    {
        //"IRequest" sirve para el ingreso d datos como la data q va a retornar, en este caso retorna un "InstructorModel"
        public class Ejecuta : IRequest<InstructorModel>
        {

            public Guid Id { get; set; }

        }

        //En este caso retorna un "InstructorModel"
        public class Manejador : IRequestHandler<Ejecuta, InstructorModel>
        {
            //Como trabajamos con Repositorio d instructores tenemos q hacer Inyeccion de dependecia d ese Obj.
            private readonly IInstructor _instructorRepository;


            //instructorRepository es el q va entrar 
            public Manejador(IInstructor instructorRepository)
            {
                //Se crea el obj.
                _instructorRepository = instructorRepository;

            }

            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //ObtenerPorId le pasamos un param q es el q viene dl request.Id y "_instructorRepository" me devuelve un InstructorModel
                var instructor = await _instructorRepository.ObtenerPorId(request.Id);

                if (instructor == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el instructor" });
                }
                return instructor;

            }
        }
    }
}