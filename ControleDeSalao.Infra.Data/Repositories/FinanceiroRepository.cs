using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class FinanceiroRepository : RepositoryBase<Financeiro>, IFinanceiroRepository
    {
        public async Task<int> Save(Financeiro obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                Db.Financeiros.Add(obj);
            }
            else
            {
                Financeiro objDb = Db.Financeiros.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.PlanoDeContaId = obj.PlanoDeContaId;
                    objDb.ClienteId = obj.ClienteId;
                    objDb.FornecedorId = obj.FornecedorId;
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.ContaFixaId = obj.ContaFixaId;
                    objDb.Tipo = obj.Tipo;
                    objDb.DataVencimento = obj.DataVencimento;
                    objDb.Valor = obj.Valor;
                    objDb.FormaDePagamento = obj.FormaDePagamento;
                    objDb.DataPagamento = obj.DataPagamento;
                    objDb.ValorPago = obj.ValorPago;
                    objDb.Status = obj.Status;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<Financeiro> GetById(int id)
        {
            return await Db.Financeiros.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Financeiro>> GetAll()
        {
            return await Db.Financeiros.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusFinanceiro.Excluida).ToListAsync();
        }

        public async Task<IEnumerable<Financeiro>> GetPagoByYear(int year)
        {
            return
                await
                    Db.Financeiros.Where(
                        m =>
                            m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusFinanceiro.Excluida &&
                            m.DataPagamento != null).ToListAsync();
        }

        public async Task<IEnumerable<Financeiro>> GetByVencimento(Enum.TipoCreditoDebito? tipo, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal)
        {
            return
                await
                    Db.Financeiros.Where(
                        m =>
                            m.EmpresaId == Cookies.IdEmpresaLogada.Value && 
                            (m.Tipo == tipo || tipo == null) &&
                            m.DataVencimento >= dataVencimentoInicial &&
                            m.DataVencimento <= dataVencimentoFinal &&
                            m.Status != Enum.StatusFinanceiro.Excluida).ToListAsync();
        }

        public async Task<IEnumerable<Financeiro>> GetByPagamento(Enum.TipoCreditoDebito? tipo, DateTime? dataPagamentoInicial, DateTime? dataPagamentoFinal)
        {
            return
                await
                    Db.Financeiros.Where(
                        m =>
                            m.EmpresaId == Cookies.IdEmpresaLogada.Value &&
                            (m.Tipo == tipo || tipo == null) &&
                            m.DataPagamento >= dataPagamentoInicial &&
                            m.DataPagamento <= dataPagamentoFinal &&
                            m.Status != Enum.StatusFinanceiro.Excluida).ToListAsync();
        }

        public async Task<IEnumerable<Financeiro>> GetAVencer(int contaFixaId)
        {
            return
                await
                    Db.Financeiros.Where(
                        m =>
                            m.ContaFixaId == contaFixaId && m.EmpresaId == Cookies.IdEmpresaLogada.Value &&
                            m.Status != Enum.StatusFinanceiro.Excluida &&
                            m.DataVencimento >= DateTime.Today).ToListAsync();
        }
    }
}