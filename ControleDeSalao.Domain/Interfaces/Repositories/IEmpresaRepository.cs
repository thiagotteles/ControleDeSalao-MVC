using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa>
    {
        Task<int> Save(Empresa obj);
        Task<Empresa> GetById(int id);
    }
}
