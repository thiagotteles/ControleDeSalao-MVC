using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ProfissionalCategoriaService : ServiceBase<ProfissionalCategoria>, IProfissionalCategoriaService
    {
        private readonly IProfissionalCategoriaRepository _repository;

        public ProfissionalCategoriaService(IProfissionalCategoriaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(ProfissionalCategoria obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<ProfissionalCategoria>> GetByProfissionalId(int profissionalId)
        {
            return await _repository.GetByProfissionalId(profissionalId);
        }

        public async Task Remove(ProfissionalCategoria obj)
        {
            await _repository.Remove(obj);
        }
    }
}
