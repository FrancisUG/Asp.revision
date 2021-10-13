using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IInstructor
    {

        //**** 1
        //Creamos una var q represent la factoria d conexion
        private readonly IFactoryConnection _factoryConnection;


        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;


        }

        //********


        public async Task<int> Actualiza(Guid instructorId, string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_editar";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultados = await connection.ExecuteAsync(

                    storeProcedure,
                    new
                    {
                        // InstructorId pertenece a un valor del SP y toma el valor q viene dsd el metodo "instructorId"
                        InstructorId = instructorId,
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Titulo = titulo
                    },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return resultados;

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo editar la data del instructor", e);
            }
        }

        public async Task<int> Elimina(Guid id)
        {
            var storeProcedure = "usp_instructor_elimina";

            try
            {

                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                     storeProcedure,

                     new
                     {
                         InstructorId = id
                     },
                     commandType: CommandType.StoredProcedure
                 );

                _factoryConnection.CloseConnection();
                return resultado;

            }
            catch (Exception e)
            {

                throw new Exception("No se pudo eliminar el instructor", e);

            }
        }



        public async Task<int> Nuevo(string nombre, string apellidos, string titulo)
        {
            //1ro definimos el nombre dl procedimiento
            var storeProcedure = "usp_instructor_nuevo";

            //2do creamos una secuencia tryCatch
            try
            {
                //Obtenemos la conexion
                var connection = _factoryConnection.GetConnection();

                var resultado = await connection.ExecuteAsync(
                storeProcedure,

                new
                {
                    InstructorId = Guid.NewGuid(),

                    //El sgte. valor es el nombe dl instructor, etc
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Titulo = titulo

                },
                //El tipo de dato, ose le indicamos q se trata d 1 procedimiento almacenado. 
                commandType: CommandType.StoredProcedure
                );

                //Indicamos q _factoryConnection cierre la conexion tambien.
                _factoryConnection.CloseConnection();

                return resultado;

            }
            catch (Exception e)
            {

                throw new Exception("No se pudo guardar el instructor", e);

            }
        }


        //*** 2
        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            //Creamos un obj q contendra toda la lista d resultados 
            IEnumerable<InstructorModel> instructorList = null;

            //Indicamos cual va ser el nombre dl P. almacenado en SQLServer
            var storeProcedure = "usp_Obtener_Instructores";

            try
            {
                //Llamamos al obj connection q vienes dsd el _factoryConnection.GetDbConnection
                var connection = _factoryConnection.GetConnection();

                //Llamamos a 1 metodo d conexion d se llama connection.QueryAsync
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);




            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);

            }
            finally
            {
                //Cerramos la conexion
                _factoryConnection.CloseConnection();

            }
            return instructorList;

        }
        //***** Con esto culminamos la llamada al StoreProcedure a travéz d la clase "InstructorModel"





        public async Task<InstructorModel> ObtenerPorId(Guid id)
        {
            var storeProcedure = "usp_obtener_instructor_por_id";

            InstructorModel instructor = null;

            try
            {
                var connection = _factoryConnection.GetConnection();

                instructor = await connection.QueryFirstAsync<InstructorModel>(

                    storeProcedure,
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.StoredProcedure
                );


                return instructor;

            }
            catch (Exception e)
            {

                throw new Exception("No se pudo encontrar el instructor", e);

            }
        }
    }

}
