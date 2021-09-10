using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IClienteAppService : IAppServiceBase<Cliente>
    {
        Task<int> Save(Cliente obj);
        Task<IEnumerable<Cliente>> GetAll();
        Task<IEnumerable<Cliente>> AutoComplete(string nome);
        Task<Cliente> GetById(int id);
        Task<Cliente> GetByNome(string nome);
    }
}
