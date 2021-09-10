using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ProfissionalCategoriaAppService : AppServiceBase<ProfissionalCategoria>, IProfissionalCategoriaAppService
    {
        private readonly IProfissionalCategoriaService _service;

        public ProfissionalCategoriaAppService(IProfissionalCategoriaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(ProfissionalCategoria obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<ProfissionalCategoria>> GetByProfissionalId(int profissionalId)
        {
            return await _service.GetByProfissionalId(profissionalId);
        }

        public async Task Remove(ProfissionalCategoria obj)
        {
            await _service.Remove(obj);
        }
    }
}
