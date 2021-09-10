using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class CompraDetalheRepository : RepositoryBase<CompraDetalhe>, ICompraDetalheRepository
    {
        public async Task<int> Save(CompraDetalhe obj)
        {
            if (obj.Id == 0)
            {
                Db.ComprasDetalhes.Add(obj);
            }
            else
            {
                var objDb = Db.ComprasDetalhes.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.CompraId = obj.CompraId;
                    objDb.ProdutoId = obj.ProdutoId;
                    objDb.Quantidade = obj.Quantidade;
                    objDb.ValorProduto = obj.ValorProduto;
                    objDb.ValorTotal = obj.ValorTotal;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<CompraDetalhe> GetById(int id)
        {
            return await Db.ComprasDetalhes.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId)
        {
            return await Db.ComprasDetalhes.Where(m => m.CompraId == compraId).ToListAsync();
        }

        public async Task<int> Remove(CompraDetalhe obj)
        {
            var objDb = Db.ComprasDetalhes.Find(obj.Id);
            if (objDb != null)
            {
                Db.ComprasDetalhes.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }
    }
}
