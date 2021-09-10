using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IComandaPagamentoService : IServiceBase<ComandaPagamento>
    {
        Task<int> Save(ComandaPagamento obj);
        Task<ComandaPagamento> GetById(int id);
        Task<ComandaPagamento> GetByComandaId(int comandaId);
        Task<int> Remove(ComandaPagamento obj);
    }
}
