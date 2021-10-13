using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;


//PaginacionRepositorio xsi solo no puede viajar hasta el ViewAPI primero necesita ser consumido por 
//Una capa de negocio, POR ESO CREAMOS DSPS LA CLASE "PaginacionCurso"
namespace Persistencia.DapperConexion.Paginacion
{
    //------->>>>>IMPLEMENTAMOS LA INTERFAZ
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection factoryConnection)
        {
            //Inyectamos la conexion
            _factoryConnection = factoryConnection;

        }

        //------->>>>>IMPLEMENTAMOS EL METODO
        //Metodo generico q puede ser utilizado por diferentes entidades
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            //Hacemos la transaccion a la Db usando SP
            PaginacionModel paginacionModel = new PaginacionModel();

            // 'listaReporte' se llenara con la data q viene dsd la DB PERO  lo q viene x defaul es un IEnumerable x eso convertimos mas abajo
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;

            try
            {
                //Ya tenemos la conexion creada  
                var connection = _factoryConnection.GetConnection();

                //Creamos un obj q represente los param q voy a pasar, se lo ubica en dond se ejecuta el SP
                DynamicParameters parametros = new DynamicParameters();

                //Bucle q va leer la data dl param 'parametrosFiltro'.. 
                foreach (var param in parametrosFiltro)
                {
                    //Siempre todos los param q registren tienen q llevar un @... 'Key' es el nombre q le damos al param q va entrar
                    //y q se registre el valor d ese param, con esto ya insertamos todos los posibles filtros q se le pueda hacer a la logica en el SP
                    parametros.Add("@" + param.Key, param.Value);
                }

                //Agg los param q van ingresar al metodo.. numeroPagina va ir llenando a @NumeroPagina
                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElementos);
                parametros.Add("@Ordenamiento", ordenamientoColumna);

                //Agg los param de SALIDA, declaramos las var arriba
                parametros.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);

                parametros.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                //Ejecutamos el SP
                //Indicamos los param y el tipo d transaccion q es.. en este caso 'commandType: CommandType.StoredProcedure'
                var result = await connection.QueryAsync(storeProcedure, parametros, commandType: CommandType.StoredProcedure);

                //Hacemos q cada resgitro q proviene dl result se convrt en un tipo 'Dictionary' d tipo List x es = a 1 regist q vien d la tabla
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();

                //La propiedad 'ListaRecords' sera = a la conversion d toda la data q viene dsd la lista d curos d la DB
                paginacionModel.ListaRecords = listaReporte;


                //paginacionModel tiene un param q se llama 'NumeroPaginas' y sera = a lo q va retornar mi SP
                // Osea  parametros.Add("TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);
                paginacionModel.NumeroPaginas = parametros.Get<int>("@TotalPaginas");

                paginacionModel.TotalRecords = parametros.Get<int>("@TotalRecords");

            }
            catch (Exception e)
            {

                throw new Exception("No se pudo ejecutar el procedimiento almacenado", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return paginacionModel;
        }
    }
}
//PaginacionRepositorio xsi solo no puede viajar hasta el ViewAPI primero necesita ser consumido por 
//Una capa de negocio, POR ESO CREAMOS DSPS LA CLASE "PaginacionCurso"