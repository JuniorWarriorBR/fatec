using System.ComponentModel.DataAnnotations;

namespace SemTumultoApi.Models.Usuarios
{
    public class UsuarioLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
