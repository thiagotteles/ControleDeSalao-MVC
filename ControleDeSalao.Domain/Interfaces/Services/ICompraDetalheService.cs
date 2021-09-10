using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface ICompraDetalheService : IServiceBase<CompraDetalhe>
    {
        Task<int> Save(CompraDetalhe obj);
        Task<CompraDetalhe> GetById(int id);
        Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId);
        Task<int> Remove(CompraDetalhe obj);
    }
}
