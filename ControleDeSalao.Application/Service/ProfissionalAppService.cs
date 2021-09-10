using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ProfissionalAppService : AppServiceBase<Profissional>, IProfissionalAppService
    {
        private readonly IProfissionalService _service;

        public ProfissionalAppService(IProfissionalService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Profissional obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<Profissional>> AutoComplete(string nome)
        {
            return await _service.AutoComplete(nome);
        }

        public async Task<Profissional> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Profissional> GetByNome(string nome)
        {
            return await _service.GetByNome(nome);
        }

        public async Task<IEnumerable<Profissional>> GetComAgenda()
        {
            return await _service.GetComAgenda();
        }
    }
}
