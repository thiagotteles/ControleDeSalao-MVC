using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ComissaoRepository : RepositoryBase<Comissao>, IComissaoRepository
    {
        public async Task<int> Save(Comissao obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = Domain.Enums.Enum.StatusComissao.Aberto;
                Db.Comissoes.Add(obj);
            }
            else
            {
                var objDb = await Db.Comissoes.FindAsync(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.Descricao = obj.Descricao;
                    objDb.DataLancamento = obj.DataLancamento;
                    objDb.Valor = obj.Valor;
                    objDb.DataPagamento = obj.DataPagamento;
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.Status = obj.Status;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    objDb.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                    objDb.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<Comissao> GetById(int id)
        {
            return await Db.Comissoes.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissionalIdAndSatus(int profissionalId, Domain.Enums.Enum.StatusComissao status)
        {
            return await Db.Comissoes.Where(m => m.ProfissionalId == profissionalId && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Comissao>> GetByStatus(Domain.Enums.Enum.StatusComissao status)
        {
            return await Db.Comissoes.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndDate(int profissionalId, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Comissoes.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && 
                             m.ProfissionalId == profissionalId && 
                             m.Status != Domain.Enums.Enum.StatusComissao.Excluido &&
                             m.DataLancamento >= dataInicial &&
                             m.DataLancamento <= dataFinal).ToListAsync();
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndPagamento(int profissionalId, DateTime dataPagamento)
        {
            return await Db.Comissoes.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value &&
                             m.ProfissionalId == profissionalId &&
                             m.Status == Domain.Enums.Enum.StatusComissao.Pago &&
                             m.DataPagamento == dataPagamento).ToListAsync();
        }
    }
}
