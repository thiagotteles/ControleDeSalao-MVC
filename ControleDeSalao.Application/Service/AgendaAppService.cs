using System;
using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class AgendaAppService : AppServiceBase<Agenda>, IAgendaAppService
    {
        private readonly IAgendaService _service;

        public AgendaAppService(IAgendaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Agenda obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Agenda>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Agenda> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<Agenda>> GetByData(DateTime data)
        {
            return await _service.GetByData(data);
        }

        public async Task<IEnumerable<Agenda>> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return await _service.GetByPeriodo(dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndData(int profissionalId, DateTime data)
        {
            return await _service.GetByProfissionalIdAndData(profissionalId, data);
        }

        public IEnumerable<Agenda> GetByProfissionalIdAndDataNotAsync(int profissionalId, DateTime data)
        {
            return _service.GetByProfissionalIdAndDataNotAsync(profissionalId, data);
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndPeriodo(int profissionalId, DateTime dataInicial, DateTime dataFinal)
        {
            return await _service.GetByProfissionalIdAndPeriodo(profissionalId, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Agenda>> GetByComandaId(int comandaId)
        {
            return await _service.GetByComandaId(comandaId);
        }
    }
}
