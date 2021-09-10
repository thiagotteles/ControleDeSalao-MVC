using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class CompraPagamentoRepository : RepositoryBase<CompraPagamento>, ICompraPagamentoRepository
    {
        public async Task<int> Save(CompraPagamento obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                Db.ComprasPagamentos.Add(obj);
            }
            else
            {
                var objDb = Db.ComprasPagamentos.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.CompraId = obj.CompraId;
                    objDb.Valor = obj.Valor;
                    objDb.QtdParcelas = obj.QtdParcelas;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<CompraPagamento> GetById(int id)
        {
            return await Db.ComprasPagamentos.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CompraPagamento> GetByCompraId(int compraId)
        {
            return await Db.ComprasPagamentos.FirstOrDefaultAsync(m => m.CompraId == compraId);
        }

        public async Task<int> Remove(CompraPagamento obj)
        {
            var objDb = Db.ComprasPagamentos.Find(obj.Id);
            if (objDb != null)
            {
                Db.ComprasPagamentos.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }
    }
}
