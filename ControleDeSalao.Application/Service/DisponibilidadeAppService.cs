using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class DisponibilidadeAppService : AppServiceBase<Disponibilidade>, IDisponibilidadeAppService
    {
        private readonly IDisponibilidadeService _service;

        public DisponibilidadeAppService(IDisponibilidadeService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Disponibilidade obj)
        {
            return await _service.Save(obj);
        }

        public async Task<Disponibilidade> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public Disponibilidade GetAllDefaults()
        {
            return _service.GetAllDefaults();
        }
    }
}
