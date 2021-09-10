using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IComandaPagamentoAppService : IAppServiceBase<ComandaPagamento>
    {
        Task<int> Save(ComandaPagamento obj);
        Task<ComandaPagamento> GetById(int id);
        Task<ComandaPagamento> GetByComandaId(int comandaId);
        Task<int> Remove(ComandaPagamento obj);
    }
}
