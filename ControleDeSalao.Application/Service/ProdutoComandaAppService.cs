using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ProdutoComandaAppService : AppServiceBase<ProdutoComanda>, IProdutoComandaAppService
    {
        private readonly IProdutoComandaService _service;

        public ProdutoComandaAppService(IProdutoComandaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ProdutoComanda obj)
        {
            return await _service.Save(obj);
        }

        public async Task<ProdutoComanda> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByComandaId(int comandaId)
        {
            return await _service.GetByComandaId(comandaId);
        }

        public async Task<int> Remove(ProdutoComanda obj)
        {
            return await _service.Remove(obj);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByYear(int year)
        {
            return await _service.GetByYear(year);
        }

        public async Task<IEnumerable<ProdutoComanda>> GetByMonth(int year, int month)
        {
            return await _service.GetByMonth(year, month);
        }
    }
}
