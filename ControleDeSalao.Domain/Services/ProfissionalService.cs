using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ProfissionalService : ServiceBase<Profissional>, IProfissionalService
    {
        private readonly IProfissionalRepository _repository;

        public ProfissionalService(IProfissionalRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Profissional obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Profissional>> AutoComplete(string nome)
        {
            return await _repository.AutoComplete(nome);
        }

        public async Task<Profissional> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Profissional> GetByNome(string nome)
        {
            return await _repository.GetByNome(nome);
        }

        public async Task<IEnumerable<Profissional>> GetComAgenda()
        {
            return await _repository.GetComAgenda();
        }
    }
}
