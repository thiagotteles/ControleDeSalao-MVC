using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IEmpresaAppService : IAppServiceBase<Empresa>
    {
        Task<int> Save(Empresa obj);
        Task<Empresa> GetById(int id);
    }
}
