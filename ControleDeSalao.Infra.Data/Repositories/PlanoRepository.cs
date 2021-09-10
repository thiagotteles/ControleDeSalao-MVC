using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class PlanoRepository : RepositoryBase<Plano>, IPlanoRepository
    {
        public async Task<IEnumerable<Plano>> GetAll()
        {
            return await Db.Planos.ToListAsync();
        }

        public async Task<Plano> GetById(int id)
        {
            return await Db.Planos.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
