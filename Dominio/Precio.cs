using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Precio
    {
        public Guid PrecioId {get;set;}

        //Edit la longitud de decimal q tendrá PrecioActual y Promocion
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual {get;set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal Promocion {get;set;}
        public Guid CursoId {get;set;}
        
    }
}