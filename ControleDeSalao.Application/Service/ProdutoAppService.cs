using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ProdutoAppService : AppServiceBase<Produto>, IProdutoAppService
    {
        private readonly IProdutoService _service;

        public ProdutoAppService(IProdutoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Produto obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Produto>> AutoComplete(string nome)
        {
            return await _service.AutoComplete(nome);
        }

        public async Task<Produto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Produto> GetByNome(string nome)
        {
            return await _service.GetByNome(nome);
        }

        public async Task<IEnumerable<Produto>> GetComPoucoEstoque()
        {
            return await _service.GetComPoucoEstoque();
        }
    }
}
