using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IProfissionalCategoriaRepository : IRepositoryBase<ProfissionalCategoria>
    {
        Task<int> Save(ProfissionalCategoria obj);
        Task<IEnumerable<ProfissionalCategoria>> GetByProfissionalId(int profissionalId);
        Task Remove(ProfissionalCategoria obj);
    }
}
