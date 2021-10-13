using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        //Este metodo me va retornar 1 obj d tipo PaginacionModel, tendra los sgtes. param 
        //Cuanoo ejecut este metodo llamara al SP y devolverara una list d records, ttal d records y # d pagns
        Task<PaginacionModel> devolverPaginacion(
         string storeProcedure,
         int numeroPagina,
         int cantidadElementos,


         //Param d filtros en donde 'IDictionary' almacenara datos d tipo string y object y el nombre dl param sera
         //'parametrosFiltro'
         IDictionary<string,object> parametrosFiltro,
         string ordenamientoColumna

         );
    }
}

            //---->>>> ESTOS ATRIBUTOS LOS TENDRA "PaginacionModel"
        //Lista d records, es la data q queremos 
        //Ttal d records es el total d registros q hay dentro d la tabla Cursos
        // Y numero de paginas es = A cuantos grupos d cursos se estan creando 