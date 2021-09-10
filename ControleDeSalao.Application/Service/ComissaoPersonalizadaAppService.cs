using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ComissaoPersonalizadaAppService : AppServiceBase<ComissaoPersonalizada>, IComissaoPersonalizadaAppService
    {
        private readonly IComissaoPersonalizadaService _service;

        public ComissaoPersonalizadaAppService(IComissaoPersonalizadaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ComissaoPersonalizada obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<ComissaoPersonalizada> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByProfissionalId(int profissionalId)
        {
            return await _service.GetByProfissionalId(profissionalId);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByServicoId(int servicoId)
        {
            return await _service.GetByServicoId(servicoId);
        }

        public async Task<ComissaoPersonalizada> GetByServicoIdAndProfissionalId(int servicoId, int profissionalId)
        {
            return await _service.GetByServicoIdAndProfissionalId(servicoId, profissionalId);
        }
    }
}
