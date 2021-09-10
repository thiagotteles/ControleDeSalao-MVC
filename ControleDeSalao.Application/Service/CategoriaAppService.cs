using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class CategoriaAppService : AppServiceBase<Categoria>, ICategoriaAppService
    {
        private readonly ICategoriaService _service;

        public CategoriaAppService(ICategoriaService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _service.GetById(id);
        }
    }
}
