using System;
using SemTumultoApi.Models.Empresas;
using SemTumultoApi.Models.Usuarios;

namespace SemTumultoApi.Models.Reservas
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public Guid? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public bool Concluida { get; set; } = false;
        public DateTime DataHoraReserva { get; set; }

    }
}
