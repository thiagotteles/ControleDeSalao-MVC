using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ComandaPagamentoParcelaService : ServiceBase<ComandaPagamentoParcela>, IComandaPagamentoParcelaService
    {
        private readonly IComandaPagamentoParcelaRepository _repository;

        public ComandaPagamentoParcelaService(IComandaPagamentoParcelaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(ComandaPagamentoParcela obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<ComandaPagamentoParcela> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<ComandaPagamentoParcela>> GetByComandaPagamentoId(int comandaPagamentoId)
        {
            return await _repository.GetByComandaPagamentoId(comandaPagamentoId);
        }

        public async Task<int> Remove(ComandaPagamentoParcela obj)
        {
            return await _repository.Remove(obj);
        }
    }
}
