using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class CompraAppService : AppServiceBase<Compra>, ICompraAppService
    {
        private readonly ICompraService _service;

        public CompraAppService(ICompraService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Compra obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Compra>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Compra> GetById(int id)
        {
            return await _service.GetById(id);
        }
        
        public async Task<IEnumerable<Compra>> GetByStatus(Domain.Enums.Enum.StatusCompra status)
        {
            return await _service.GetByStatus(status);
        }

        public async Task<IEnumerable<Compra>> GetByFornecedorIdAndPeriodo(int fornecedorId, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _service.GetByFornecedorIdAndPeriodo(fornecedorId, dataInicial, dataFinal);
        }
    }
}
