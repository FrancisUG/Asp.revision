using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        //Indicamos lo q queremos q retorne

        Task<IEnumerable<InstructorModel>> ObtenerLista();

        //Q devuelva tambien 1 solo Instructor

        Task<InstructorModel> ObtenerPorId(Guid id);


        //Ponemos int xq la lista q retorna es el # d transacciones
        Task<int> Nuevo(string nombre, string apellidos, string titulo);

        Task<int> Actualiza(Guid instructorId, string nombre, string apellidos, string titulo);

        Task<int> Elimina(Guid id);

    }
}
//alt-shift+ F para identar codigo.