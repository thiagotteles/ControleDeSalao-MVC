using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IHorarioFuncionamentoRepository : IRepositoryBase<HorarioFuncionamento>
    {
        Task<int> Save(HorarioFuncionamento obj);
        Task<HorarioFuncionamento> GetById(int id);
        Task<HorarioFuncionamento> GetByEmpresaId(int empresaId);
    }
}
