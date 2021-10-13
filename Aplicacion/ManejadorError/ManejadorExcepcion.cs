using System;
using System.Net;

namespace Aplicacion.ManejadorError
{
    //Esta clase hereda de la clase Exception
    public class ManejadorExcepcion : Exception 
    {
        //Inicializo 2 valores para inyectar
        public HttpStatusCode Codigo {get;}
        public object Errores {get;}

       
        //Le asignamos 2 param 1 para estado y otro para el obj d error = a null
        //"codigo" y "errores" son los parámetros q me llegan desde "WebAPI" contenidos en la Folder Middleware
        public ManejadorExcepcion(HttpStatusCode codigo, object errores = null){
            
            Codigo = codigo;
            Errores = errores;
        }
        
    }
}