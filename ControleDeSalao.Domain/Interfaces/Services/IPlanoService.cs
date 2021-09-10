using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IPlanoService : IServiceBase<Plano>
    {
        Task<IEnumerable<Plano>> GetAll();
        Task<Plano> GetById(int id);
    }
}
