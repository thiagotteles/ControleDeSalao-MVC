using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IComandaPagamentoRepository : IRepositoryBase<ComandaPagamento>
    {
        Task<int> Save(ComandaPagamento obj);
        Task<ComandaPagamento> GetById(int id);
        Task<ComandaPagamento> GetByComandaId(int comandaId);
        Task<int> Remove(ComandaPagamento obj);
    }
}
