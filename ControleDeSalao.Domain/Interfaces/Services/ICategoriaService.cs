using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface ICategoriaService : IServiceBase<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> GetById(int id);
    }
}
