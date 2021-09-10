using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<int> Save(Produto obj);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<IEnumerable<Produto>> AutoComplete(string nome);
        Task<Produto> GetByNome(string nome);
        Task<IEnumerable<Produto>> GetComPoucoEstoque();
    }
}
