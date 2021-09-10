using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class MensalidadeService : ServiceBase<Mensalidade>, IMensalidadeService
    {
        private readonly IMensalidadeRepository _repository;

        public MensalidadeService(IMensalidadeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Mensalidade obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Mensalidade>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Mensalidade> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Mensalidade>> GetByEmpresaId(int empresaId)
        {
            return await _repository.GetByEmpresaId(empresaId);
        }
    }
}
