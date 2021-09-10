using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface ICompraPagamentoService : IServiceBase<CompraPagamento>
    {
        Task<int> Save(CompraPagamento obj);
        Task<CompraPagamento> GetById(int id);
        Task<CompraPagamento> GetByCompraId(int compraId);
        Task<int> Remove(CompraPagamento obj);
    }
}
