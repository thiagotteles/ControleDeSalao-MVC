using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IServicoPreCadastradoAppService : IAppServiceBase<ServicoPreCadastrado>
    {
        Task<IEnumerable<ServicoPreCadastrado>> GetAll();
        Task<ServicoPreCadastrado> GetById(int id);
    }
}
