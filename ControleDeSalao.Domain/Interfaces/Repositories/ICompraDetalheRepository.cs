using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface ICompraDetalheRepository : IRepositoryBase<CompraDetalhe>
    {
        Task<int> Save(CompraDetalhe obj);
        Task<CompraDetalhe> GetById(int id);
        Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId);
        Task<int> Remove(CompraDetalhe obj);
    }
}
