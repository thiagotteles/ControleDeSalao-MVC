using System;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ComissaoPersonalizadaService : ServiceBase<ComissaoPersonalizada>, IComissaoPersonalizadaService
    {
        private readonly IComissaoPersonalizadaRepository _repository;

        public ComissaoPersonalizadaService(IComissaoPersonalizadaRepository repository)
            : base(repository)
        {
            _repository = repository;
        
        }

        public async Task<int> Save(ComissaoPersonalizada obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ComissaoPersonalizada> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByProfissionalId(int profissionalId)
        {
            return await _repository.GetByProfissionalId(profissionalId);
        }

        public async Task<IEnumerable<ComissaoPersonalizada>> GetByServicoId(int servicoId)
        {
            return await _repository.GetByServicoId(servicoId);
        }

        public async Task<ComissaoPersonalizada> GetByServicoIdAndProfissionalId(int servicoId, int profissionalId)
        {
            return await _repository.GetByServicoIdAndProfissionalId(servicoId, profissionalId);
        }
    }
}
