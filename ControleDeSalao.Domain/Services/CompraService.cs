using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class CompraService : ServiceBase<Compra>, ICompraService
    {
        private readonly ICompraRepository _repository;

        public CompraService(ICompraRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Compra obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Compra>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Compra> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Compra>> GetByStatus(Enums.Enum.StatusCompra status)
        {
            return await _repository.GetByStatus(status);
        }

        public async Task<IEnumerable<Compra>> GetByFornecedorIdAndPeriodo(int fornecedorId, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetByFornecedorIdAndPeriodo(fornecedorId, dataInicial, dataFinal);
        }
    }
}
