using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ServicoAppService : AppServiceBase<Servico>, IServicoAppService
    {
        private readonly IServicoService _service;

        public ServicoAppService(IServicoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Servico obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Servico>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Servico>> AutoComplete(string nome)
        {
            return await _service.AutoComplete(nome);
        }

        public async Task<Servico> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Servico> GetByNome(string nome)
        {
            return await _service.GetByNome(nome);
        }

        public async Task<IEnumerable<Servico>> GetByCategorias(int[] categoriasId)
        {
            return await _service.GetByCategorias(categoriasId);
        }

        public async Task AddAllDefaults(int empresaId, int usuarioId)
        {
            await _service.AddAllDefaults(empresaId, usuarioId);
        }
    }
}
