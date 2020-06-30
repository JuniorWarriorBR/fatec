using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemTumulto.Services.JwtBearerService;
using SemTumultoApi.Data;
using SemTumultoApi.Models.Usuarios;

namespace SemTumultoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IQueryable<Usuario> query = _context.Usuarios;
            query = query.AsNoTracking().Where(u => u.Email == model.Email && u.Senha == ComputeSha256Hash(model.Senha));
            var usuario = await query.SingleOrDefaultAsync();

            if (usuario == null)
                return Unauthorized();

            var token = TokenService.GenerateToken(usuario);
            usuario.Senha = "";

            return Ok(new
            {
                usuario = UsuarioToDTO(usuario),
                token = token
            });
        }


        // GET: api/Usuario
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            return await _context.Usuarios
                    .Select(u => UsuarioToDTO(u))
                    .ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return UsuarioToDTO(usuario);
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            // Ajustar
            // if (UsuarioEmailExists(usuario.Email))
            //     return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, UsuarioToDTO(usuario));
        }

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (UsuarioEmailExists(usuario.Email))
                return BadRequest();

            usuario.Senha = ComputeSha256Hash(usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, UsuarioToDTO(usuario));
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Senha = "";

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }

        private bool UsuarioEmailExists(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }

        private static UsuarioDTO UsuarioToDTO(Usuario usuario) =>
        new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
