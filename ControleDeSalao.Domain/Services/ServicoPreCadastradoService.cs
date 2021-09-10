using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ServicoPreCadastradoService : ServiceBase<ServicoPreCadastrado>, IServicoPreCadastradoService
    {
        private readonly IServicoPreCadastradoRepository _repository;

        public ServicoPreCadastradoService(IServicoPreCadastradoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServicoPreCadastrado>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServicoPreCadastrado> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
