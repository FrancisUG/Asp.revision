using System;

namespace Aplicacion.Cursos
{
    public class InstructorDto
    {
        public Guid InstructorId {get;set;}
        public string Nombre {get;set;}
        public string Apellidos {get;set;}
        public string Grado {get;set;}
        public byte[] FotoPerfil{get;set;}
        public DateTime? FechaCreacion{get;set;}
        //InstructorDto funciona dentro de la clase "Consulta" xq cuando consultamos 1 curso nos devuelve la lista
        //de instructores, comentarios y precios
        //En la folder Instructores la clase "Consulta" devuelv la data q viene dsd el SP y quien MAPEA la data
        //d SP de Instructor ? es la clase InstructorModel--->> Nos vamos a "InstructorModel"
        
    }
}