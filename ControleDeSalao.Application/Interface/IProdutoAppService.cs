using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IProdutoAppService : IAppServiceBase<Produto>
    {
        Task<int> Save(Produto obj);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<IEnumerable<Produto>> AutoComplete(string nome);
        Task<Produto> GetByNome(string nome);
        Task<IEnumerable<Produto>> GetComPoucoEstoque();
    }
}
