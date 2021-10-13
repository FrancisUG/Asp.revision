using Dominio;    
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

namespace Persistencia
{
    
    public class CursosOnlineContext : IdentityDbContext<Usuario>
    {  
       
        public CursosOnlineContext(DbContextOptions options) : base(options) {
            //CursoInstructor tiene llave compuesta, aqui hacemos esa representacion
        }

        

            //Este metodo proviene dsd el padre, heredado por eso va override
            protected override void OnModelCreating(ModelBuilder modelBuilder){


                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<CursoInstructor>().HasKey(ci => new {ci.InstructorId, ci.CursoId});

            }

            //Envoltorio de entidad a cada una de las clases...
            //Mapeo de clases, representacion de mi DB
            public DbSet<Comentario> Comentario{get;set;}
            public DbSet<Curso> Curso{get;set;}
            public DbSet<CursoInstructor> CursoInstructor{get;set;}
            public DbSet<Instructor> Instructor{get;set;}
            public DbSet<Precio> Precio{get;set;}

    }


}
