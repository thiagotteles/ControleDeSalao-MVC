using System;
using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IFinanceiroAppService : IAppServiceBase<Financeiro>
    {
        Task<int> Save(Financeiro obj);
        Task<Financeiro> GetById(int id);
        Task<IEnumerable<Financeiro>> GetAll();
        Task<IEnumerable<Financeiro>> GetPagoByYear(int year);
        Task<IEnumerable<Financeiro>> GetByVencimento(Domain.Enums.Enum.TipoCreditoDebito? tipo, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal);
        Task<IEnumerable<Financeiro>> GetByPagamento(Domain.Enums.Enum.TipoCreditoDebito? tipo, DateTime? dataPagamentoInicial, DateTime? dataPagamentoFinal);
        Task<IEnumerable<Financeiro>> GetAVencer(int contaFixaId);
    }
}
