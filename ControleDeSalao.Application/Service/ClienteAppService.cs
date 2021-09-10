using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ClienteAppService : AppServiceBase<Cliente>, IClienteAppService
    {
        private readonly IClienteService _service;

        public ClienteAppService(IClienteService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Cliente obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Cliente>> AutoComplete(string nome)
        {
            return await _service.AutoComplete(nome);
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Cliente> GetByNome(string nome)
        {
            return await _service.GetByNome(nome);
        }
    }
}
