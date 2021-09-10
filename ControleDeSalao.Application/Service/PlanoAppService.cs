using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class PlanoAppService : AppServiceBase<Plano>, IPlanoAppService
    {
        private readonly IPlanoService _service;

        public PlanoAppService(IPlanoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Plano>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Plano> GetById(int id)
        {
            return await _service.GetById(id);
        }
    }
}
