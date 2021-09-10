using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IProfissionalCategoriaAppService : IAppServiceBase<ProfissionalCategoria>
    {
        Task<int> Save(ProfissionalCategoria obj);
        Task<IEnumerable<ProfissionalCategoria>> GetByProfissionalId(int profissionalId);
        Task Remove(ProfissionalCategoria obj);
    }
}
