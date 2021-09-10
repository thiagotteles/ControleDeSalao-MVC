using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class MensalidadeAppService : AppServiceBase<Mensalidade>, IMensalidadeAppService
    {
        private readonly IMensalidadeService _service;

        public MensalidadeAppService(IMensalidadeService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Mensalidade obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Mensalidade>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Mensalidade> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<Mensalidade>> GetByEmpresaId(int empresaId)
        {
            return await _service.GetByEmpresaId(empresaId);
        }
    }
}
