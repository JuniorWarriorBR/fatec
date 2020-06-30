using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemTumultoApi.Models.Reservas;
using SemTumultoApi.Repositories.Reservas;

namespace SemTumultoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read([FromServices] IReservaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var reservas = repository.Read(id);
            return Ok(reservas);
        }

        [HttpGet]
        [Route("empresa")]
        public IActionResult ReadByEmpresa([FromServices] IReservaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var reservas = repository.ReadByEmpresa(id);
            return Ok(reservas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Reserva model, [FromServices] IReservaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.UsuarioId = new Guid(User.Identity.Name);

            repository.Create(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Reserva model, [FromServices] IReservaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Update(new Guid(id), model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromServices] IReservaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Delete(new Guid(id));
            return Ok();
        }
    }
}
