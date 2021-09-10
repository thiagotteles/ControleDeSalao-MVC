using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ServicoComandaService : ServiceBase<ServicoComanda>, IServicoComandaService
    {
        private readonly IServicoComandaRepository _repository;

        public ServicoComandaService(IServicoComandaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(ServicoComanda obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<ServicoComanda> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<ServicoComanda>> GetByComandaId(int comandaId)
        {
            return await _repository.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ServicoComanda obj)
        {
            return await _repository.Remove(obj);
        }

        public async Task<IEnumerable<ServicoComanda>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<ServicoComanda>> GetByYear(int year)
        {
            return await _repository.GetByYear(year);
        }
        public async Task<IEnumerable<ServicoComanda>> GetByMonth(int year, int month)
        {
            return await _repository.GetByMonth(year, month);
        }
    }
}
