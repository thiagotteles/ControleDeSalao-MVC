using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class HorarioFuncionamentoService : ServiceBase<HorarioFuncionamento>, IHorarioFuncionamentoService
    {
        private readonly IHorarioFuncionamentoRepository _repository;

        public HorarioFuncionamentoService(IHorarioFuncionamentoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(HorarioFuncionamento obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<HorarioFuncionamento> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<HorarioFuncionamento> GetByEmpresaId(int empresaId)
        {
            return await _repository.GetByEmpresaId(empresaId);
        }
    }
}
