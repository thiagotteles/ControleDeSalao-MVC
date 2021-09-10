using ControleDeSalao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IAgendaAppService : IAppServiceBase<Agenda>
    {
        Task<int> Save(Agenda obj);
        Task<IEnumerable<Agenda>> GetAll();
        Task<Agenda> GetById(int id);
        Task<IEnumerable<Agenda>> GetByData(DateTime data);
        Task<IEnumerable<Agenda>> GetByPeriodo(DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Agenda>> GetByProfissionalIdAndData(int profissionalId, DateTime data);
        IEnumerable<Agenda> GetByProfissionalIdAndDataNotAsync(int profissionalId, DateTime data);
        Task<IEnumerable<Agenda>> GetByProfissionalIdAndPeriodo(int profissionalId, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Agenda>> GetByComandaId(int comandaId);
    }
}
