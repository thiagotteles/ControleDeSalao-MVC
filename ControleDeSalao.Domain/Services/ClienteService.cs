using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Cliente obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Cliente>> AutoComplete(string nome)
        {
            return await _repository.AutoComplete(nome);
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Cliente> GetByNome(string nome)
        {
            return await _repository.GetByNome(nome);
        }
    }
}
