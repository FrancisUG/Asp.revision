using System;

namespace Dominio
{
    public class Comentario
    {
        //"Guid Global unique identifier" tipo d dato para Id a nivel Enterprise y no d aplicacion.
        //Aleatoriamnete genera un valor único, lo mismo en "CursoId"
        public Guid ComentarioId {get;set;}
        public string Alumno {get;set;}
        public int Puntaje{get;set;}
        public string ComentarioTexto {get;set;}
        public Guid CursoId {get;set;}
        public DateTime? FechaCreacion{get;set;}



        public Curso Curso {get;set;}
        
    }
}
//Creamos una clase dentro de mi proyecto Dominio q represent el user de mi APP llamada Usuario