using System;
using System.Collections.Generic;
using SemTumultoApi.Models.Reservas;

namespace SemTumultoApi.Repositories.Reservas
{
    public interface IReservaRepository
    {
        List<Reserva> Read(Guid id);
        List<Reserva> ReadByEmpresa(Guid id);
        void Create(Reserva reserva);
        void Delete(Guid id);
        void Update(Guid id, Reserva reserva);
    }
}
