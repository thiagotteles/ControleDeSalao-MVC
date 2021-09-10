using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class CompraDetalheAppService : AppServiceBase<CompraDetalhe>, ICompraDetalheAppService
    {
        private readonly ICompraDetalheService _service;

        public CompraDetalheAppService(ICompraDetalheService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(CompraDetalhe obj)
        {
            return await _service.Save(obj);
        }

        public async Task<CompraDetalhe> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId)
        {
            return await _service.GetByCompraId(compraId);
        }

        public async Task<int> Remove(CompraDetalhe obj)
        {
            return await _service.Remove(obj);
        }
    }
}
