using System;
using System.Collections.Generic;
using SemTumultoApi.Models.Empresas;

namespace SemTumultoApi.Repositories.Empresas
{
    public interface IEmpresaRepository
    {
        List<Empresa> ReadAll();
        List<Empresa> Read(Guid id);
        void Create(Empresa empresa);
        void Delete(Guid id);
        void Update(Guid id, Empresa empresa);
    }
}
