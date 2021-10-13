using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI.Controllers
{

    //     http://localhost:5000/api/Cursos  -->> Endpoint para el controller

    //Una clase de tipo Controller debe tener una notacion

    [Route("api/[controller]")]

    //Ler indicamos q se trata de un apiController
    [ApiController]


    public class CursosController : MiControllerBase
    {



        //Método GET para poder devolver la lista de cursos

        [HttpGet]


        public async Task<ActionResult<List<CursoDto>>> Get()
        {

            return await Mediator.Send(new Consulta.ListaCursos());
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<CursoDto>> Detalle(Guid id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });

        }

        //  ***** Metodos siguientes...

        [HttpPost]

        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }



        //Le indicamos el parámetro q va a entrar de tipo id
        [HttpPut("{id}")]

        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
        {
            // data.CursoId es = al id q me manda el usuario
            data.CursoId = id;


            return await Mediator.Send(data);

        }

        //Le indicamos q ese valor va a ser parte d la Url
        [HttpDelete("{id}")]




        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {



            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });



        }

        //Como necesitamos pasarle un Json INDICANDO q queremos la pag #1 por eso va POST
        [HttpPost("report")]


        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCurso.Ejecuta data)
        {

            return await Mediator.Send(data);
        }

    }
}
