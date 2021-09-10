using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class CompraPagamentoParcelaRepository : RepositoryBase<CompraPagamentoParcela>, ICompraPagamentoParcelaRepository
    {
        public async Task<int> Save(CompraPagamentoParcela obj)
        {
            if (obj.Id == 0)
            {
                Db.ComprasPagamentosParcelas.Add(obj);
            }
            else
            {
                var objDb = Db.ComprasPagamentosParcelas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.CompraPagamentoId = obj.CompraPagamentoId;
                    objDb.DataVencimento = obj.DataVencimento;
                    objDb.Valor = obj.Valor;
                    objDb.FormaDePagamento = obj.FormaDePagamento;
                    objDb.PlanoDeContaId = obj.PlanoDeContaId;
                    objDb.FinanceiroId = obj.FinanceiroId;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<CompraPagamentoParcela> GetById(int id)
        {
            return await Db.ComprasPagamentosParcelas.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraPagamentoId)
        {
            return await Db.ComprasPagamentosParcelas.Where(m => m.CompraPagamentoId == compraPagamentoId).ToListAsync();
        }

        public async Task<int> Remove(CompraPagamentoParcela obj)
        {
            var objDb = Db.ComprasPagamentosParcelas.Find(obj.Id);
            if (objDb != null)
            {
                Db.ComprasPagamentosParcelas.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }
    }
}
