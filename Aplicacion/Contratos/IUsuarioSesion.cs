namespace Aplicacion.Contratos
{
    public interface IUsuarioSesion
    {
        //Creamos un metodo q devolvera un string q represente user en usuario sesion

        string ObtenerUsuarioSesion();

        // ----->>>> Nos vamos al proyecto Seguridad a IMPLEMENTAR esta interfaz.... 
        //Creamos una clase "UsuarioSesion" en Seguridad.TokenSeguridad
         
    }
}