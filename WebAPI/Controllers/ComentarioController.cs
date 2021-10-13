using System;
using System.Threading.Tasks;
using Aplicacion.Comentarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //Al pasarle la herencia d "MiControllerBase" la clase ya se conviert en Controller
    public class ComentarioController : MiControllerBase
    {
        [HttpPost]

        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {

            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {

            //Id va alimentarse d la variable Guid id q viene dsd el cliente
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });

        }


    }


}