using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class PlanoDeContaAppService : AppServiceBase<PlanoDeConta>, IPlanoDeContaAppService
    {
        private readonly IPlanoDeContaService _service;

        public PlanoDeContaAppService(IPlanoDeContaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(PlanoDeConta obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<PlanoDeConta>> GetAll(bool withInativo = false)
        {
            return await _service.GetAll(withInativo);
        }

        public async Task<PlanoDeConta> GetById(int id, bool withInativo = false)
        {
            return await _service.GetById(id, withInativo);
        }

        public async Task<PlanoDeConta> GetByTelaPadrao(Domain.Enums.Enum.TelaPadrao telaPadrao, bool withInativo = false)
        {
            return await _service.GetByTelaPadrao(telaPadrao, withInativo);
        }

        public async Task<IEnumerable<PlanoDeConta>> GetByTipo(Domain.Enums.Enum.TipoCreditoDebito tipo, bool withInativo = false)
        {
            return await _service.GetByTipo(tipo, withInativo);
        }

        public async Task AddAllDefaults(int empresaId, int usuarioId)
        {
            await _service.AddAllDefaults(empresaId, usuarioId);
        }
    }
}
