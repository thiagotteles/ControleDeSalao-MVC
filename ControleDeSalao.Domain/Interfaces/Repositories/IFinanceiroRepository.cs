using ControleDeSalao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IFinanceiroRepository : IRepositoryBase<Financeiro>
    {
        Task<int> Save(Financeiro obj);
        Task<Financeiro> GetById(int id);
        Task<IEnumerable<Financeiro>> GetAll();
        Task<IEnumerable<Financeiro>> GetPagoByYear(int year);
        Task<IEnumerable<Financeiro>> GetByVencimento(Enums.Enum.TipoCreditoDebito? tipo, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal);
        Task<IEnumerable<Financeiro>> GetByPagamento(Enums.Enum.TipoCreditoDebito? tipo, DateTime? dataPagamentoInicial, DateTime? dataPagamentoFinal);
        Task<IEnumerable<Financeiro>> GetAVencer(int contaFixaId);
    }
}
