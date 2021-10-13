using System;

namespace Dominio
{
    public class CursoInstructor
    {
        public Guid CursoId {get;set;}
        public Curso Curso {get;set;}
        public Guid InstructorId {get;set;}
        
        //ancla de instructor, osea "CursoInstructor" llama a la tabla "Instructor"
        public Instructor Instructor {get;set;}
        
    }
}