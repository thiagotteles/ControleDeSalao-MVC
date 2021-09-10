using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IHorarioFuncionamentoAppService : IAppServiceBase<HorarioFuncionamento>
    {
        Task<int> Save(HorarioFuncionamento obj);
        Task<HorarioFuncionamento> GetById(int id);
        Task<HorarioFuncionamento> GetByEmpresaId(int empresaId);
    }
}
