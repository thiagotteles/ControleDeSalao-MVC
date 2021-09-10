using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class CompraPagamentoParcelaAppService : AppServiceBase<CompraPagamentoParcela>, ICompraPagamentoParcelaAppService
    {
        private readonly ICompraPagamentoParcelaService _service;

        public CompraPagamentoParcelaAppService(ICompraPagamentoParcelaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(CompraPagamentoParcela obj)
        {
            return await _service.Save(obj);
        }

        public async Task<CompraPagamentoParcela> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<CompraPagamentoParcela>> GetByCompraPagamentoId(int compraId)
        {
            return await _service.GetByCompraPagamentoId(compraId);
        }

        public async Task<int> Remove(CompraPagamentoParcela obj)
        {
            return await _service.Remove(obj);
        }
    }
}
