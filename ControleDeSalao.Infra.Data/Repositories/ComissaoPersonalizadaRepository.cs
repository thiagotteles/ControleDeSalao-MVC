using System;
using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ControleDeSalao.Infra.Cross.Security;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ComissaoPersonalizadaRepository : RepositoryBase<ComissaoPersonalizada>, IComissaoPersonalizadaRepository
    {
        public async Task<int> Save(ComissaoPersonalizada obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.ComissoesPersonalizadas.Add(obj);
            }
            else
            {
                var objDb = Db.ComissoesPersonalizadas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.ServicoId = obj.ServicoId;
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.Comissao = obj.Comissao;
                    objDb.Status = obj.Status;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    objDb.IdUsuarioAlteracao = obj.IdUsuarioAlteracao;
                    objDb.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                    objDb.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetAll()
        {
            return await Db.ComissoesPersonalizadas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<ComissaoPersonalizada> GetById(int id)
        {
            return await Db.ComissoesPersonalizadas.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByProfissionalId(int profissionalId)
        {
            return await Db.ComissoesPersonalizadas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ProfissionalId == profissionalId).ToListAsync();
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByServicoId(int servicoId)
        {
            return await Db.ComissoesPersonalizadas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ServicoId == servicoId).ToListAsync();
        }

        public async Task<ComissaoPersonalizada> GetByServicoIdAndProfissionalId(int servicoId, int profissionalId)
        {
            return await Db.ComissoesPersonalizadas.FirstOrDefaultAsync(m => m.ServicoId == servicoId && m.ProfissionalId == profissionalId && m.Status);
        }
    }
}
