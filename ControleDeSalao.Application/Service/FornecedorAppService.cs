using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class FornecedorAppService : AppServiceBase<Fornecedor>, IFornecedorAppService
    {
        private readonly IFornecedorService _service;

        public FornecedorAppService(IFornecedorService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Fornecedor obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Fornecedor>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Fornecedor>> AutoComplete(string nome)
        {
            return await _service.AutoComplete(nome);
        }

        public async Task<Fornecedor> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Fornecedor> GetByNome(string nome)
        {
            return await _service.GetByNome(nome);
        }
    }
}
