using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class CompraPagamentoParcelaService : ServiceBase<CompraPagamentoParcela>, ICompraPagamentoParcelaService
    {
        private readonly ICompraPagamentoParcelaRepository _repository;

        public CompraPagamentoParcelaService(ICompraPagamentoParcelaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(CompraPagamentoParcela obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<CompraPagamentoParcela> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraId)
        {
            return await _repository.GetByCompraPagamentoId(compraId);
        }

        public async Task<int> Remove(CompraPagamentoParcela obj)
        {
            return await _repository.Remove(obj);
        }
    }
}
