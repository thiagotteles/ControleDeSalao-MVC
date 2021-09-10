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
    public class ServicoRepository : RepositoryBase<Servico>, IServicoRepository
    {
        public async Task<int> Save(Servico obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = obj.EmpresaId > 0 ? obj.EmpresaId : Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = obj.IdUsuarioCadastro > 0 ? obj.IdUsuarioCadastro : Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Servicos.Add(obj);
            }
            else
            {
                var objDb = Db.Servicos.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Descricao = obj.Descricao;
                    objDb.Codigo = obj.Codigo;
                    objDb.CategoriaId = obj.CategoriaId;
                    objDb.ServicoPreCadastradoId = obj.ServicoPreCadastradoId;
                    objDb.Valor = obj.Valor;
                    objDb.ValorParaProfissional = obj.ValorParaProfissional;
                    objDb.HoraDuracao = obj.HoraDuracao;
                    objDb.MinutoDuracao = obj.MinutoDuracao;
                    objDb.Observacao = obj.Observacao;
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

        public async Task<IEnumerable<Servico>> GetAll()
        {
            return await Db.Servicos.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Servico>> AutoComplete(string nome)
        {
            return await Db.Servicos.Where(m => m.Descricao.ToLower().Contains(nome) && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).Take(10).ToListAsync();
        }

        public async Task<Servico> GetById(int id)
        {
            return await Db.Servicos.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Servico> GetByNome(string nome)
        {
            return await Db.Servicos.FirstOrDefaultAsync(m => m.Descricao == nome && m.Status && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Servico>> GetByCategorias(int[] categoriasId)
        {
            //return await Db.Servicos.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && categoriasId.Contains(m.CategoriaId)).ToListAsync();
            return await Db.Servicos.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }
    }
}
