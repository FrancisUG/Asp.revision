using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Paginacion;

namespace Aplicacion.Cursos
{
    public class PaginacionCurso
    {
        //retorna un tipo de dato 'PaginacionModel'
        public class Ejecuta : IRequest<PaginacionModel>
        {

            //Mi paginacion filtrara por Titulo, NumeroPagina, cantidadElementos
            public string  Titulo { get; set; }

            //Param q represent el # d pag
            public int NumeroPagina { get; set; }

            //Cuantos elementos queremos q se impriman en esa pagina
            public int CantidadElementos { get; set; }

        }


    //Creamos la clase q implement la logica
        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {

            private readonly IPaginacion _paginacion;

            //Creamos el obj repositorio
            public Manejador(IPaginacion paginacion)
            {
                _paginacion = paginacion;
            }

            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Indicamos el nombre dl SP a ejecutar, el encargado d la paginacion para el curso
                var storeProcedure = "usp_obtener_curso_paginacion";

                //La forma d ornamiento sera asc o desc PERO sera por Titulo, podemos escoger cualquir columna d la Db q se pueda ordenar
                var ordenamiento = "Titulo";

                //Para pasarle un array con los param d filtro q viene dsd PaginacionRepositorio, creamos un diccionario
                var parametros = new Dictionary<string,object>();

                //Indicamos q pase el param 'NombreCurso' y el valor q entra como param d filtro es 'Titulo'
                //Dentro dl array parametrosFiltro q es el d arriba 'var parametros = new Dictionary<string,object>();'
                // le add un nuevo param q se llama 'NombreCurso'
                //y debera existir en el SP tambien
                parametros.Add("NombreCurso", request.Titulo);


                return await _paginacion.devolverPaginacion(storeProcedure, request.NumeroPagina, request.CantidadElementos, parametros, ordenamiento);

            }
        }
    }
}