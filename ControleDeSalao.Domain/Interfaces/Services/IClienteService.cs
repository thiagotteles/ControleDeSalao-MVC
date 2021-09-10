using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        Task<int> Save(Cliente obj);
        Task<IEnumerable<Cliente>> GetAll();
        Task<IEnumerable<Cliente>> AutoComplete(string nome);
        Task<Cliente> GetById(int id);
        Task<Cliente> GetByNome(string nome);
    }
}
