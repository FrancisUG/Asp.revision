using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>> { }


        public class Manejador : IRequestHandler<Lista, List<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;


            public Manejador(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;

            }

            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                //Almacenamos estos valores en "resultado"
                var resultado = await _instructorRepository.ObtenerLista();

                //El IEnumerable q venia en resultado lo convertimos en tipo lista 

                return resultado.ToList();
                //El IEnumerable q venia en resultado lo convertimos en TIPO Lista
            }
        }

    }
}
