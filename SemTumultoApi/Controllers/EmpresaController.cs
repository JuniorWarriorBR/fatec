using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemTumultoApi.Models.Empresas;
using SemTumultoApi.Repositories.Empresas;

namespace SemTumultoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        [HttpGet]
        // [AllowAnonymous]
        public IActionResult Read([FromServices] IEmpresaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var tarefas = repository.Read(id);
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("listar-empresas")]
        [AllowAnonymous]
        public IActionResult ReadAll([FromServices] IEmpresaRepository repository)
        {
            var tarefas = repository.ReadAll();
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Empresa model, [FromServices] IEmpresaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.UsuarioId = new Guid(User.Identity.Name);

            repository.Create(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Empresa model, [FromServices] IEmpresaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Update(new Guid(id), model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromServices] IEmpresaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Delete(new Guid(id));
            return Ok();
        }
    }
}
