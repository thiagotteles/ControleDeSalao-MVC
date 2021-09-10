using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Application.Service
{
    public class FinanceiroAppService : AppServiceBase<Financeiro>, IFinanceiroAppService
    {
        private readonly IFinanceiroService _service;

        public FinanceiroAppService(IFinanceiroService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Financeiro obj)
        {
            return await _service.Save(obj);
        }

        public async Task<Financeiro> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<Financeiro>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Financeiro>> GetPagoByYear(int year)
        {
            return await _service.GetPagoByYear(year);
        }

        public async Task<IEnumerable<Financeiro>> GetByVencimento(Enum.TipoCreditoDebito? tipo, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal)
        {
            return await _service.GetByVencimento(tipo, dataVencimentoInicial, dataVencimentoFinal);
        }

        public async Task<IEnumerable<Financeiro>> GetByPagamento(Enum.TipoCreditoDebito? tipo, DateTime? dataPagamentoInicial, DateTime? dataPagamentoFinal)
        {
            return await _service.GetByPagamento(tipo, dataPagamentoInicial, dataPagamentoFinal);
        }

        public async Task<IEnumerable<Financeiro>> GetAVencer(int contaFixaId)
        {
            return await _service.GetAVencer(contaFixaId);
        }
    }
}