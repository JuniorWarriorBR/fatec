using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SemTumultoApi.Data;
using SemTumultoApi.Models.Empresas;

namespace SemTumultoApi.Repositories.Empresas
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DataContext _context;
        public EmpresaRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Empresa empresa)
        {
            empresa.Id = Guid.NewGuid();
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var empresa = _context.Empresas.Find(id);
            _context.Entry(empresa).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Empresa> Read(Guid id)
        {
            IQueryable<Empresa> query = _context.Empresas
            .Include(e => e.Reservas).ThenInclude(u => u.Usuario)
            .Include(u => u.Usuario);


            query = query.AsNoTracking()
                        .Where(empresa => empresa.UsuarioId == id);

            return query.ToList();
            // return _context.Empresas.Where(empresa => empresa.UsuarioId == id).ToList();
        }

        public List<Empresa> ReadAll()
        {
            return _context.Empresas.ToList();
        }

        public void Update(Guid id, Empresa empresa)
        {
            var _empresa = _context.Empresas.Find(id);

            _empresa.Nome = empresa.Nome;
            _empresa.Descricao = empresa.Descricao;

            _context.Entry(_empresa).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
