using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Produto obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Produto>> AutoComplete(string nome)
        {
            return await _repository.AutoComplete(nome);
        }

        public async Task<Produto> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Produto> GetByNome(string nome)
        {
            return await _repository.GetByNome(nome);
        }

        public async Task<IEnumerable<Produto>> GetComPoucoEstoque()
        {
            return await _repository.GetComPoucoEstoque();
        }
    }
}
