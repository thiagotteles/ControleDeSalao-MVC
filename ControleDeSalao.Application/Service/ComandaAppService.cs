using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ComandaAppService : AppServiceBase<Comanda>, IComandaAppService
    {
        private readonly IComandaService _service;

        public ComandaAppService(IComandaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Comanda obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Comanda>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Comanda> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalID(int profissionalID)
        {
            return await _service.GetByProfissionalID(profissionalID);
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndData(Domain.Enums.Enum.StatusComanda status, System.DateTime? data)
        {
            return await _service.GetByStatusAndData(status, data);
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndPeriodo(Domain.Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _service.GetByStatusAndPeriodo(status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetByClienteIdStatusAndPeriodo(int clienteId, Domain.Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _service.GetByClienteIdStatusAndPeriodo(clienteId, status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalIdStatusAndPeriodo(int profissionalId, Domain.Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _service.GetByProfissionalIdStatusAndPeriodo(profissionalId, status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetDescontoByStatusAndPeriodo(Domain.Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _service.GetDescontoByStatusAndPeriodo(status, dataInicial, dataFinal);
        }
    }
}
