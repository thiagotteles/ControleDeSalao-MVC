using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IComandaPagamentoParcelaRepository : IRepositoryBase<ComandaPagamentoParcela>
    {
        Task<int> Save(ComandaPagamentoParcela obj);
        Task<ComandaPagamentoParcela> GetById(int id);
        Task<IEnumerable<ComandaPagamentoParcela>> GetByComandaPagamentoId(int comandaPagamentoId);
        Task<int> Remove(ComandaPagamentoParcela obj);
    }
}
