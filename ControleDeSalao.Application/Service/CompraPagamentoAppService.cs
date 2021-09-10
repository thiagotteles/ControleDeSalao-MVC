using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class CompraPagamentoAppService : AppServiceBase<CompraPagamento>, ICompraPagamentoAppService
    {
        private readonly ICompraPagamentoService _service;

        public CompraPagamentoAppService(ICompraPagamentoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(CompraPagamento obj)
        {
            return await _service.Save(obj);
        }

        public async Task<CompraPagamento> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<CompraPagamento> GetByCompraId(int compraId)
        {
            return await _service.GetByCompraId(compraId);
        }

        public async Task<int> Remove(CompraPagamento obj)
        {
            return await _service.Remove(obj);
        }
    }
}
