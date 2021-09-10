using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ProdutoComandaService : ServiceBase<ProdutoComanda>, IProdutoComandaService
    {
        private readonly IProdutoComandaRepository _repository;

        public ProdutoComandaService(IProdutoComandaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(ProdutoComanda obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<ProdutoComanda> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByComandaId(int comandaId)
        {
            return await _repository.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ProdutoComanda obj)
        {
            return await _repository.Remove(obj);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByYear(int year)
        {
            return await _repository.GetByYear(year);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByMonth(int year, int month)
        {
            return await _repository.GetByMonth(year, month);
        }
    }
}
