using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ServicoPreCadastradoAppService : AppServiceBase<ServicoPreCadastrado>, IServicoPreCadastradoAppService
    {
        private readonly IServicoPreCadastradoService _service;

        public ServicoPreCadastradoAppService(IServicoPreCadastradoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ServicoPreCadastrado>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<ServicoPreCadastrado> GetById(int id)
        {
            return await _service.GetById(id);
        }
    }
}
