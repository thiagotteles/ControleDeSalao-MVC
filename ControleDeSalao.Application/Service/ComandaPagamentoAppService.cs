using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ComandaPagamentoAppService : AppServiceBase<ComandaPagamento>, IComandaPagamentoAppService
    {
        private readonly IComandaPagamentoService _service;

        public ComandaPagamentoAppService(IComandaPagamentoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ComandaPagamento obj)
        {
            return await _service.Save(obj);
        }

        public async Task<ComandaPagamento> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<ComandaPagamento> GetByComandaId(int comandaId)
        {
            return await _service.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ComandaPagamento obj)
        {
            return await _service.Remove(obj);
        }
    }
}
