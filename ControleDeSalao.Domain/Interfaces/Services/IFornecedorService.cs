using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IFornecedorService : IServiceBase<Fornecedor>
    {
        Task<int> Save(Fornecedor obj);
        Task<IEnumerable<Fornecedor>> GetAll();
        Task<Fornecedor> GetById(int id);
        Task<IEnumerable<Fornecedor>> AutoComplete(string nome);
        Task<Fornecedor> GetByNome(string nome);
    }
}
