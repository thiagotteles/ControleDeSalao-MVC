using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IEmpresaService : IServiceBase<Empresa>
    {
        Task<int> Save(Empresa obj);
        Task<Empresa> GetById(int id);
    }
}
