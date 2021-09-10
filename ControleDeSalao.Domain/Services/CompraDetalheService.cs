using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class CompraDetalheService : ServiceBase<CompraDetalhe>, ICompraDetalheService
    {
        private readonly ICompraDetalheRepository _repository;

        public CompraDetalheService(ICompraDetalheRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(CompraDetalhe obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<CompraDetalhe> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<CompraDetalhe>> GetByCompraId(int compraId)
        {
            return await _repository.GetByCompraId(compraId);
        }

        public async Task<int> Remove(CompraDetalhe obj)
        {
            return await _repository.Remove(obj);
        }
    }
}
