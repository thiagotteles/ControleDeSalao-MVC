using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class HorarioFuncionamentoAppService : AppServiceBase<HorarioFuncionamento>, IHorarioFuncionamentoAppService
    {
        private readonly IHorarioFuncionamentoService _service;

        public HorarioFuncionamentoAppService(IHorarioFuncionamentoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(HorarioFuncionamento obj)
        {
            return await _service.Save(obj);
        }

        public async Task<HorarioFuncionamento> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<HorarioFuncionamento> GetByEmpresaId(int empresaId)
        {
            return await _service.GetByEmpresaId(empresaId);
        }
    }
}
