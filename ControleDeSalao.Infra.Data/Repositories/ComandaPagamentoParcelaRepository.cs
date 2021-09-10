using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ComandaPagamentoParcelaRepository : RepositoryBase<ComandaPagamentoParcela>, IComandaPagamentoParcelaRepository
    {
        public async Task<int> Save(ComandaPagamentoParcela obj)
        {
            if (obj.Id == 0)
            {
                Db.ComandasPagamentosParcelas.Add(obj);
            }
            else
            {
                var objDb = Db.ComandasPagamentosParcelas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.ComandaPagamentoId = obj.ComandaPagamentoId;
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

        public async Task<ComandaPagamentoParcela> GetById(int id)
        {
            return await Db.ComandasPagamentosParcelas.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<ComandaPagamentoParcela>> GetByComandaPagamentoId(int comandaPagamentoId)
        {
            return await Db.ComandasPagamentosParcelas.Where(m => m.ComandaPagamentoId == comandaPagamentoId).ToListAsync();
        }

        public async Task<int> Remove(ComandaPagamentoParcela obj)
        {
            var objDb = Db.ComandasPagamentosParcelas.Find(obj.Id);
            if (objDb != null)
            {
                Db.ComandasPagamentosParcelas.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }
    }
}
