using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using ControleDeSalao.Infra.Cross.Security;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class PlanoDeContaRepository : RepositoryBase<PlanoDeConta>, IPlanoDeContaRepository
    {
        public async Task<int> Save(PlanoDeConta obj)
        {
            if (obj.TelaPadrao != Enum.TelaPadrao.Nenhuma)
            {
                var plan = Db.PlanoDeContas.Where(m => (m.TelaPadrao == obj.TelaPadrao && m.Id != obj.Id && m.EmpresaId == obj.EmpresaId));
                if (plan.Any())
                {
                    foreach (var planoDeConta in plan)
                    {
                        planoDeConta.TelaPadrao = Enum.TelaPadrao.Nenhuma;
                        planoDeConta.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                        planoDeConta.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                    }
                }
            }

            if (obj.Id == 0)
            {
                obj.EmpresaId = obj.EmpresaId > 0 ? obj.EmpresaId : Cookies.IdEmpresaLogada.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.PlanoDeContas.Add(obj);
            }
            else
            {
                var pl = Db.PlanoDeContas.Find(obj.Id);
                if (pl != null)
                {
                    pl.EmpresaId = obj.EmpresaId;
                    pl.Nome = obj.Nome;
                    pl.Tipo = obj.Tipo;
                    pl.TelaPadrao = obj.TelaPadrao;
                    pl.Observacao = obj.Observacao;
                    pl.Status = obj.Status;
                    pl.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    pl.IdUsuarioAlteracao = obj.IdUsuarioAlteracao;
                    pl.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                    pl.IdUsuarioAlteracao = obj.IdUsuarioAlteracao;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<PlanoDeConta>> GetAll(bool withInativo = false)
        {
            return await Task.Run(() => Db.PlanoDeContas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && (m.Status || withInativo)));
        }

        public async Task<PlanoDeConta> GetById(int id, bool withInativo = false)
        {
            return await Db.PlanoDeContas.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value && (m.Status || withInativo));
        }

        public async Task<PlanoDeConta> GetByTelaPadrao(Enum.TelaPadrao telaPadrao, bool withInativo = false)
        {
            return await Db.PlanoDeContas.FirstOrDefaultAsync(m => m.TelaPadrao == telaPadrao && m.EmpresaId == Cookies.IdEmpresaLogada.Value && (m.Status || withInativo));
        }

        public async Task<IEnumerable<PlanoDeConta>> GetByTipo(Enum.TipoCreditoDebito tipo, bool withInativo = false)
        {
            return await Task.Run(() => Db.PlanoDeContas.Where(m =>  m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Tipo == tipo && (m.Status || withInativo)));
        }
    }
}
