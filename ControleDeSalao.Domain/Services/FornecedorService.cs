using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class FornecedorService : ServiceBase<Fornecedor>, IFornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Fornecedor obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Fornecedor>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Fornecedor>> AutoComplete(string nome)
        {
            return await _repository.AutoComplete(nome);
        }

        public async Task<Fornecedor> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Fornecedor> GetByNome(string nome)
        {
            return await _repository.GetByNome(nome);
        }
    }
}
