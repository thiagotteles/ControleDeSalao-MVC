using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IPlanoAppService : IAppServiceBase<Plano>
    {
        Task<IEnumerable<Plano>> GetAll();
        Task<Plano> GetById(int id);
    }
}
