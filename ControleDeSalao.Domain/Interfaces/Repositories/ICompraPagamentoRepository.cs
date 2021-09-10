using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface ICompraPagamentoRepository : IRepositoryBase<CompraPagamento>
    {
        Task<int> Save(CompraPagamento obj);
        Task<CompraPagamento> GetById(int id);
        Task<CompraPagamento> GetByCompraId(int compraId);
        Task<int> Remove(CompraPagamento obj);
    }
}
