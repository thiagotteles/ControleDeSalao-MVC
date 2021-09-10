using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface ICompraPagamentoParcelaService : IServiceBase<CompraPagamentoParcela>
    {
        Task<int> Save(CompraPagamentoParcela obj);
        Task<CompraPagamentoParcela> GetById(int id);
        Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraId);
        Task<int> Remove(CompraPagamentoParcela obj);
    }
}
