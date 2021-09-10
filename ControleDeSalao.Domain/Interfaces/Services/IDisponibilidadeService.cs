using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IDisponibilidadeService : IServiceBase<Disponibilidade>
    {
        Task<int> Save(Disponibilidade obj);
        Task<Disponibilidade> GetById(int id);
        Disponibilidade GetAllDefaults();
    }
}
