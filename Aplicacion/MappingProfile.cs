using System.Linq;
using Aplicacion.Cursos;
using AutoMapper;
using Dominio;

namespace Aplicacion
{
    //Heredera dsd Profile e importamos dsd la liberia q importamos
    public class MappingProfile : Profile
    {

        //Creamos el constructor
        public MappingProfile()
        {

            CreateMap<Curso, CursoDto>()


            .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructoresLink.Select(a => a.Instructor).ToList()))

            .ForMember(x=> x.Comentarios, y=> y.MapFrom(z=> z.ComentarioLista))
            .ForMember(x=> x.Precio, y=> y.MapFrom(z=> z.PrecioPromocion));


            CreateMap<CursoInstructor, CursoInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comentario, ComentarioDto>();
            CreateMap<Precio, PrecioDto>();
            

            

            //Posterior pasamos a la clase "Consulta"


        }
    }
}