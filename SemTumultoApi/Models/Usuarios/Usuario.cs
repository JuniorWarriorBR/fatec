using System;
using System.ComponentModel.DataAnnotations;

namespace SemTumultoApi.Models.Usuarios
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Este campo deve conter no mínimo 6 caracteres")]
        public string Senha { get; set; }

    }
}
