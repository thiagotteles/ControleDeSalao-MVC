using System;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class AgendaService : ServiceBase<Agenda>, IAgendaService
    {
        private readonly IAgendaRepository _repository;

        public AgendaService(IAgendaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Agenda obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Agenda>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Agenda> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Agenda>> GetByData(DateTime data)
        {
            return await _repository.GetByData(data);
        }

        public async Task<IEnumerable<Agenda>> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return await _repository.GetByPeriodo(dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndData(int profissionalId, DateTime data)
        {
            return await _repository.GetByProfissionalIdAndData(profissionalId, data);
        }

        public IEnumerable<Agenda> GetByProfissionalIdAndDataNotAsync(int profissionalId, DateTime data)
        {
            return _repository.GetByProfissionalIdAndDataNotAsync(profissionalId, data);
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndPeriodo(int profissionalId, DateTime dataInicial, DateTime dataFinal)
        {
            return await _repository.GetByProfissionalIdAndPeriodo(profissionalId, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Agenda>> GetByComandaId(int comandaId)
        {
            return await _repository.GetByComandaId(comandaId);
        }
    }
}
