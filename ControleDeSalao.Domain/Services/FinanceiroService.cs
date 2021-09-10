using System;
using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Domain.Services
{
    public class FinanceiroService : ServiceBase<Financeiro>, IFinanceiroService
    {
        private readonly IFinanceiroRepository _repository;

        public FinanceiroService(IFinanceiroRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Financeiro obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<Financeiro> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Financeiro>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Financeiro>> GetPagoByYear(int year)
        {
            return await _repository.GetPagoByYear(year);
        }

        public async Task<IEnumerable<Financeiro>> GetByVencimento(Enum.TipoCreditoDebito? tipo, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal)
        {
            return await _repository.GetByVencimento(tipo, dataVencimentoInicial, dataVencimentoFinal);
        }

        public async Task<IEnumerable<Financeiro>> GetByPagamento(Enum.TipoCreditoDebito? tipo, DateTime? dataPagamentoInicial, DateTime? dataPagamentoFinal)
        {
            return await _repository.GetByPagamento(tipo, dataPagamentoInicial, dataPagamentoFinal);
        }

        public async Task<IEnumerable<Financeiro>> GetAVencer(int contaFixaId)
        {
            return await _repository.GetAVencer(contaFixaId);
        }
    }
}
