using System.Collections.Generic;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {

        //Retornara una data generica q autom se mapee se convierta en Json y lo devuelva direct al client
        //retorna un arreglo de tipo 'IDictionary'
        public List<IDictionary<string,object>> ListaRecords { get; set; }

        //Ttal d records es el total d registros q hay dentro d la tabla Cursos
        public int TotalRecords { get; set; }

        //Y numero de paginas es = A cuantos grupos d cursos se estan creando
        public int NumeroPaginas { get; set; }

    }
}
