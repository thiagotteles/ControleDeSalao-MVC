using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class PromocaoAppService : AppServiceBase<Promocao>, IPromocaoAppService
    {
        private readonly IPromocaoService _service;

        public PromocaoAppService(IPromocaoService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<Promocao> GetByCodigoPromocional(string codigoPromocional)
        {
            return await _service.GetByCodigoPromocional(codigoPromocional);
        }
    }
}
