using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ComandaService : ServiceBase<Comanda>, IComandaService
    {
        private readonly IComandaRepository _repository;

        public ComandaService(IComandaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Comanda obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Comanda>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Comanda> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalID(int profissionalID)
        {
            return await _repository.GetByProfissionalID(profissionalID);
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndData(Enums.Enum.StatusComanda status, System.DateTime? data)
        {
            return await _repository.GetByStatusAndData(status, data);
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndPeriodo(Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetByStatusAndPeriodo(status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetByClienteIdStatusAndPeriodo(int clienteId, Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetByClienteIdStatusAndPeriodo(clienteId, status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalIdStatusAndPeriodo(int profissionalId, Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetByProfissionalIdStatusAndPeriodo(profissionalId, status, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comanda>> GetDescontoByStatusAndPeriodo(Enums.Enum.StatusComanda status, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetDescontoByStatusAndPeriodo(status, dataInicial, dataFinal);
        }
    }
}
