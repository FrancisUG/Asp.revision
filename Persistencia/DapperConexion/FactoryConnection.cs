using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;


        //Ya tenmos acceso a la cadena de conexion
        private readonly IOptions<ConexionConfiguracion> _configs;

        public FactoryConnection(IOptions<ConexionConfiguracion> configs)
        {
            _configs = configs;

        }
        public void CloseConnection()
        {
            //Evaluamos 
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {

            //Evaluamos si la cadena d conexion existe o no
            if (_connection == null)
            {

                _connection = new SqlConnection(_configs.Value.DefaultConnection);
            }

            //Evaluamos el estado d la cadena
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();

            }
            //Posterior retornamos el obj _connection.
            return _connection;
        }
    }
}

