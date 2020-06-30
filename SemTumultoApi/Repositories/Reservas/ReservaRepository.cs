using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SemTumultoApi.Data;
using SemTumultoApi.Models.Reservas;
using SemTumultoApi.Models.Usuarios;

namespace SemTumultoApi.Repositories.Reservas
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly DataContext _context;
        public ReservaRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Reserva reserva)
        {
            reserva.Id = Guid.NewGuid();
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var reserva = _context.Reservas.Find(id);
            _context.Entry(reserva).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Reserva> Read(Guid id)
        {

            IQueryable<Reserva> query = _context.Reservas
            .Include(e => e.Empresa)
            .Include(u => u.Usuario);


            query = query.AsNoTracking()
                        .OrderByDescending(c => c.DataHoraReserva)
                        .Where(reserva => reserva.UsuarioId == id);

            return query.ToList();

            // return _context.Reservas
            //     .Include(r => r.Empresa)
            //     .Where(reserva => reserva.UsuarioId == id)
            //     .OrderBy(r => r.DataHoraReserva)
            //     .ToList();
        }

        public List<Reserva> ReadByEmpresa(Guid id)
        {

            IQueryable<Reserva> query = _context.Reservas
            .Include(e => e.Empresa)
            .Include(u => u.Usuario);


            query = query.AsNoTracking()
                        .OrderByDescending(c => c.DataHoraReserva)
                        .Where(reserva => reserva.UsuarioId == id && reserva.Empresa.UsuarioId == id);

            return query.ToList();
        }

        public void Update(Guid id, Reserva reserva)
        {
            var _reserva = _context.Reservas.Find(id);

            _reserva.Concluida = reserva.Concluida;

            _context.Entry(_reserva).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private static UsuarioDTO UsuarioToDTO(Usuario usuario) =>
        new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
