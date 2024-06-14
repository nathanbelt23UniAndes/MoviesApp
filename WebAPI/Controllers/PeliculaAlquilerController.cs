using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.AppPeliculaAlquiler;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaAlquilerController : MiControllerBase
    {

        [HttpPost("Crear")]
        public async Task<ActionResult<ResponseOperations>> Crear(CrearAlquilerPelicula.CrearAlquilerPeliculaParametros data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("ConsultaPeliculaUsuario")]
        public async Task<ActionResult<List<PeliculaAlquiler>>> ConsultaPeliculaUsuario(ConsultaAlquilerPelicula.ParametroConsultaAlquilerPelicula data)
        {
          
            return await Mediator.Send(data);
        }


        [HttpPost("Cancelar")]
        public async Task<ActionResult<ResponseOperations>> Cancelar(CancelarAlquilerPelicula.CancelarAlquilerPeliculaParametros data)
        {
            return await Mediator.Send(data);
        }


        [HttpPost("Devolver")]
        public async Task<ActionResult<ResponseOperations>> Devolver(EntregarPeliculaAlquiler.EntregarPeliculaAlquilerParametros data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("Recibir")]
        public async Task<ActionResult<ResponseOperations>> RecibirPelicula(RecibidaPeliculaAlquiler.RecibidaPeliculaAlquilerParametros data)
        {
            return await Mediator.Send(data);
        }

    }
}
