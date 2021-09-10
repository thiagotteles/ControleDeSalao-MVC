using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class EmpresaService : ServiceBase<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Empresa obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<Empresa> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
