using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SemTumultoApi.Models.Reservas;
using SemTumultoApi.Models.Usuarios;

namespace SemTumultoApi.Models.Empresas
{
    public class Empresa
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Descricao { get; set; }
        public List<Reserva> Reservas { get; set; }
        public Usuario Usuario { get; set; }

    }
}
