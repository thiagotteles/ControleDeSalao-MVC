using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await Db.Categorias.Where(m => m.Status).ToListAsync();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await Db.Categorias.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
