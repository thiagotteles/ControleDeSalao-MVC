using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IDisponibilidadeRepository : IRepositoryBase<Disponibilidade>
    {
        Task<int> Save(Disponibilidade obj);
        Task<Disponibilidade> GetById(int id);
    }
}
