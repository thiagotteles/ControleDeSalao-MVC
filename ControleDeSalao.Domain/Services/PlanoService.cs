using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class PlanoService : ServiceBase<Plano>, IPlanoService
    {
        private readonly IPlanoRepository _repository;

        public PlanoService(IPlanoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Plano>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Plano> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
