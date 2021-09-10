using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IServicoRepository : IRepositoryBase<Servico>
    {
        Task<int> Save(Servico obj);
        Task<IEnumerable<Servico>> GetAll();
        Task<IEnumerable<Servico>> AutoComplete(string nome);
        Task<Servico> GetById(int id);
        Task<Servico> GetByNome(string nome);
        Task<IEnumerable<Servico>> GetByCategorias(int[] categoriasId);
    }
}
