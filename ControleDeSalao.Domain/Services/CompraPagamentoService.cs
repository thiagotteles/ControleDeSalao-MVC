using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class CompraPagamentoService : ServiceBase<CompraPagamento>, ICompraPagamentoService
    {
        private readonly ICompraPagamentoRepository _repository;

        public CompraPagamentoService(ICompraPagamentoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(CompraPagamento obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<CompraPagamento> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<CompraPagamento> GetByCompraId(int compraId)
        {
            return await _repository.GetByCompraId(compraId);
        }

        public async Task<int> Remove(CompraPagamento obj)
        {
            return await _repository.Remove(obj);
        }
    }
}
