using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IPlanoRepository : IRepositoryBase<Plano>
    {
        Task<IEnumerable<Plano>> GetAll();
        Task<Plano> GetById(int id);
    }
}
