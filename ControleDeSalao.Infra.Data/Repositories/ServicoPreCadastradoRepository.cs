using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ServicoPreCadastradoRepository : RepositoryBase<ServicoPreCadastrado>, IServicoPreCadastradoRepository
    {
        public async Task<IEnumerable<ServicoPreCadastrado>> GetAll()
        {
            return await Db.ServicosPreCadastrados.Where(m => m.Status).ToListAsync();
        }

        public async Task<ServicoPreCadastrado> GetById(int id)
        {
            return await Db.ServicosPreCadastrados.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
