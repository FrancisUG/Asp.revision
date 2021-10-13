using System;
using System.Collections.Generic;

namespace Aplicacion.Cursos
{
    public class CursoDto
    {
        //Recordar import la libreria using System;
        public Guid CursoId {get;set;}
        public string Titulo {get;set;}
        public string Descripcion {get;set;}
        public DateTime? FechaPublicacion {get;set;}
        public byte[] FotoPortada{get;set;}
   
        public ICollection<InstructorDto> Instructores {get;set;}
        
        public PrecioDto Precio {get;set;}
        public DateTime? FechaCreacion{get;set;}
        //De lo contrario no mapeara dsd Curso q es mi projct Modelo

        public ICollection<ComentarioDto> Comentarios {get;set;}



    }   

}