using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface ICompraPagamentoParcelaRepository : IRepositoryBase<CompraPagamentoParcela>
    {
        Task<int> Save(CompraPagamentoParcela obj);
        Task<CompraPagamentoParcela> GetById(int id);
        Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraId);
        Task<int> Remove(CompraPagamentoParcela obj);
    }
}
