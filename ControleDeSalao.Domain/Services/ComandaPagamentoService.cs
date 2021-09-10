using System.Threading.Tasks;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;

namespace ControleDeSalao.Domain.Services
{
    public class ComandaPagamentoService : ServiceBase<ComandaPagamento>, IComandaPagamentoService
    {
        private readonly IComandaPagamentoRepository _repository;

        public ComandaPagamentoService(IComandaPagamentoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(ComandaPagamento obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<ComandaPagamento> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ComandaPagamento> GetByComandaId(int comandaId)
        {
            return await _repository.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ComandaPagamento obj)
        {
            return await _repository.Remove(obj);
        }
    }
}
