using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IProfissionalRepository : IRepositoryBase<Profissional>
    {
        Task<int> Save(Profissional obj);
        Task<IEnumerable<Profissional>> GetAll();
        Task<IEnumerable<Profissional>> GetComAgenda();
        Task<IEnumerable<Profissional>> AutoComplete(string nome);
        Task<Profissional> GetById(int id);
        Task<Profissional> GetByNome(string nome);
    }
}
