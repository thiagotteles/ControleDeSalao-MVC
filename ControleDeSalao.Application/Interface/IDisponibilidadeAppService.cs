using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IDisponibilidadeAppService : IAppServiceBase<Disponibilidade>
    {
        Task<int> Save(Disponibilidade obj);
        Task<Disponibilidade> GetById(int id);
        Disponibilidade GetAllDefaults();
    }
}
