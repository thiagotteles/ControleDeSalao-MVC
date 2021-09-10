using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IServicoPreCadastradoRepository : IRepositoryBase<ServicoPreCadastrado>
    {
        Task<IEnumerable<ServicoPreCadastrado>> GetAll();
        Task<ServicoPreCadastrado> GetById(int id);
    }
}
