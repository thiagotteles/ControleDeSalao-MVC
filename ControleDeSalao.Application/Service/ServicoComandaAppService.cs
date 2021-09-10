using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ServicoComandaAppService : AppServiceBase<ServicoComanda>, IServicoComandaAppService
    {
        private readonly IServicoComandaService _service;

        public ServicoComandaAppService(IServicoComandaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ServicoComanda obj)
        {
            return await _service.Save(obj);
        }

        public async Task<ServicoComanda> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<ServicoComanda>> GetByComandaId(int comandaId)
        {
            return await _service.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ServicoComanda obj)
        {
            return await _service.Remove(obj);
        }

        public async Task<IEnumerable<ServicoComanda>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<IEnumerable<ServicoComanda>> GetByYear(int year)
        {
            return await _service.GetByYear(year);
        }

        public async Task<IEnumerable<ServicoComanda>> GetByMonth(int year, int month)
        {
            return await _service.GetByMonth(year, month);
        }
    }
}
