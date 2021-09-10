using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IComandaPagamentoParcelaService : IServiceBase<ComandaPagamentoParcela>
    {
        Task<int> Save(ComandaPagamentoParcela obj);
        Task<ComandaPagamentoParcela> GetById(int id);
        Task<IEnumerable<ComandaPagamentoParcela>> GetByComandaPagamentoId(int comandaPagamentoId);
        Task<int> Remove(ComandaPagamentoParcela obj);
    }
}
