using System.Data;

namespace Persistencia.DapperConexion
{
    public interface IFactoryConnection
    {
        //metodo q cerrará conexiones existentes.
        void CloseConnection();


          //Devuelv un obj de conexion
          IDbConnection GetConnection();
          

    }
}