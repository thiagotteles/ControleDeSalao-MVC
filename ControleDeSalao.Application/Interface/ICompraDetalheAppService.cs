using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface ICompraDetalheAppService : IAppServiceBase<CompraDetalhe>
    {
        Task<int> Save(CompraDetalhe obj);
        Task<CompraDetalhe> GetById(int id);
        Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId);
        Task<int> Remove(CompraDetalhe obj);
    }
}
