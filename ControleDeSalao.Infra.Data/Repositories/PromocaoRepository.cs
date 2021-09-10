using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class PromocaoRepository : RepositoryBase<Empresa>, IPromocaoRepository
    {
        public async Task<Promocao> GetByCodigoPromocional(string codigoPromocional)
        {
            return await Db.Promocoes.FirstOrDefaultAsync(m => m.CodigoPromocional == codigoPromocional && m.Status);
        }
    }
}
