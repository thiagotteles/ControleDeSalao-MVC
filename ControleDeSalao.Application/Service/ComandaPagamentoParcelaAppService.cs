using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ComandaPagamentoParcelaAppService : AppServiceBase<ComandaPagamentoParcela>, IComandaPagamentoParcelaAppService
    {
        private readonly IComandaPagamentoParcelaService _service;

        public ComandaPagamentoParcelaAppService(IComandaPagamentoParcelaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ComandaPagamentoParcela obj)
        {
            return await _service.Save(obj);
        }

        public async Task<ComandaPagamentoParcela> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<ComandaPagamentoParcela>> GetByComandaPagamentoId(int comandaPagamentoId)
        {
            return await _service.GetByComandaPagamentoId(comandaPagamentoId);
        }

        public async Task<int> Remove(ComandaPagamentoParcela obj)
        {
            return await _service.Remove(obj);
        }
    }
}
