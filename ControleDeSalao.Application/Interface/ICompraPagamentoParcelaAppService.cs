using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface ICompraPagamentoParcelaAppService : IAppServiceBase<CompraPagamentoParcela>
    {
        Task<int> Save(CompraPagamentoParcela obj);
        Task<CompraPagamentoParcela> GetById(int id);
        Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraId);
        Task<int> Remove(CompraPagamentoParcela obj);
    }
}
