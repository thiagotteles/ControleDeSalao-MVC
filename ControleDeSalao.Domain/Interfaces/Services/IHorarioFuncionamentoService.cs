using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IHorarioFuncionamentoService : IServiceBase<HorarioFuncionamento>
    {
        Task<int> Save(HorarioFuncionamento obj);
        Task<HorarioFuncionamento> GetById(int id);
        Task<HorarioFuncionamento> GetByEmpresaId(int empresaId);
    }
}
