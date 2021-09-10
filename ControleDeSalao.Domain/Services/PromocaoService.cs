using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class PromocaoService : ServiceBase<Promocao>, IPromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<Promocao> GetByCodigoPromocional(string codigoPromocional)
        {
            return await _repository.GetByCodigoPromocional(codigoPromocional);
        }
    }
}
