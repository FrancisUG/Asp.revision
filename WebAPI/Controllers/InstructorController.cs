using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;



namespace WebAPI.Controllers
{
    //Indicamos q este controller viene dsd mi Controller Base
    public class InstructorController : MiControllerBase
    {
        
        [Authorize(Roles = "Admin")]
        //A partir d aki solo los usuarios q envien el token con el rol de "Admin" podran consmir este metodo 

        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores()
        {

            //Le indicamos q va retornar un valor dsd Mediator, la clase consulta la inport dsd using Aplicacion.Instructores OJO
            return await Mediator.Send(new Consulta.Lista());
        }



        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
        [HttpPost]

        //Solo retornara un Unit, Crear() tendra la clase que va parsear 
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {

            //La data q me va enviar el cliente
            return await Mediator.Send(data);

        }

        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

        // Dentro dl endpoint le tenemos q pasar como param el id del Instructor a modificar, x eso va {id}
        [HttpPut("{id}")]

        //Este metodo recibe los param q vienen dsd Editar.Ejecuta por medio de "data"
        public async Task<ActionResult<Unit>> Actualizar(Guid id, Editar.Ejecuta data)
        {
            //data.InstructorId sera igual al id q va colocar el cliente en la URL, el id 

            data.InstructorId = id;

           
            return await Mediator.Send(data);

        }

        [HttpDelete("{id}")]


        //El unico param q va entrar es el id
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            //Ejecuta tiene una propiedad q se llama Id q se alimenta de id que es lo  q el usuario envia dsd afuera
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<InstructorModel>> ObtenerPorId(Guid id)
        {

            return await Mediator.Send(new ConsultaId.Ejecuta { Id = id });
        }



    }
}