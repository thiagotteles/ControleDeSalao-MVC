using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ComandaPagamentoRepository : RepositoryBase<ComandaPagamento>, IComandaPagamentoRepository
    {
        public async Task<int> Save(ComandaPagamento obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                Db.ComandasPagamentos.Add(obj);
            }
            else
            {
                var objDb = Db.ComandasPagamentos.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.ComandaId = obj.ComandaId;
                    objDb.Valor = obj.Valor;
                    objDb.QtdParcelas = obj.QtdParcelas;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<ComandaPagamento> GetById(int id)
        {
            return await Db.ComandasPagamentos.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ComandaPagamento> GetByComandaId(int comandaId)
        {
            return await Db.ComandasPagamentos.FirstOrDefaultAsync(m => m.ComandaId == comandaId);
        }

        public async Task<int> Remove(ComandaPagamento obj)
        {
            var objDb = Db.ComandasPagamentos.Find(obj.Id);
            if (objDb != null)
            {
                Db.ComandasPagamentos.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }
    }
}
