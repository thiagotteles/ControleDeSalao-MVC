using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IServicoService : IServiceBase<Servico>
    {
        Task<int> Save(Servico obj);
        Task<IEnumerable<Servico>> GetAll();
        Task<IEnumerable<Servico>> AutoComplete(string nome);
        Task<Servico> GetById(int id);
        Task<Servico> GetByNome(string nome);
        Task<IEnumerable<Servico>> GetByCategorias(int[] categoriasId);
        Task AddAllDefaults(int empresaId, int usuarioId);
    }
}
