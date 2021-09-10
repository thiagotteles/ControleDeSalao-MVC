using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class EmpresaAppService : AppServiceBase<Empresa>, IEmpresaAppService
    {
        private readonly IEmpresaService _service;

        public EmpresaAppService(IEmpresaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Empresa obj)
        {
            return await _service.Save(obj);
        }

        public async Task<Empresa> GetById(int id)
        {
            return await _service.GetById(id);
        }
    }
}
