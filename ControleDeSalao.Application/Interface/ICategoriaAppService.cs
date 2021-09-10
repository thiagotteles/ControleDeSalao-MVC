using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface ICategoriaAppService : IAppServiceBase<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> GetById(int id);
    }
}
