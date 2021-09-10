using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
