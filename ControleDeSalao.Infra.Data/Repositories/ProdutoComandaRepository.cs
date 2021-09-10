using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Enums;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ControleDeSalao.Infra.Cross.Security;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ProdutoComandaRepository : RepositoryBase<ProdutoComanda>, IProdutoComandaRepository
    {
        public async Task<int> Save(ProdutoComanda obj)
        {
            if (obj.Id == 0)
            {
                Db.ProdutosComanda.Add(obj);
            }
            else
            {
                var objDb = Db.ProdutosComanda.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.ComandaId = obj.ComandaId;
                    objDb.ProdutoId = obj.ProdutoId;
                    objDb.Quantidade = obj.Quantidade;
                    objDb.ValorProduto = obj.ValorProduto;
                    objDb.ValorTotal = obj.ValorTotal;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<ProdutoComanda> GetById(int id)
        {
            return await Db.ProdutosComanda.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByComandaId(int comandaId)
        {
            return await Db.ProdutosComanda.Where(m => m.ComandaId == comandaId).ToListAsync();
        }

        public async Task<int> Remove(ProdutoComanda obj)
        {
            var objDb = Db.ProdutosComanda.Find(obj.Id);
            if (objDb != null)
            {
                Db.ProdutosComanda.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByYear(int year)
        {
            var comandas =
                 Db.Comandas.Where(
                     m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Data.Year == year && m.Status != Enum.StatusComanda.Excluida)
                     .Select(m => m.Id)
                     .ToArray();

            return await Db.ProdutosComanda.Where(m => comandas.Contains(m.ComandaId)).ToListAsync();
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByMonth(int year, int month)
        {
            var comandas =
             Db.Comandas.Where(
                 m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Data.Year == year && m.Data.Month == month && m.Status != Enum.StatusComanda.Excluida)
                 .Select(m => m.Id)
                 .ToArray();

            return await Db.ProdutosComanda.Where(m => comandas.Contains(m.ComandaId)).ToListAsync();
        }
    }
}
