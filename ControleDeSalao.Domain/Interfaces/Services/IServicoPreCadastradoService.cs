using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IServicoPreCadastradoService : IServiceBase<ServicoPreCadastrado>
    {
        Task<IEnumerable<ServicoPreCadastrado>> GetAll();
        Task<ServicoPreCadastrado> GetById(int id);
    }
}
