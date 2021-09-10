using System;
using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ComissaoService : ServiceBase<Comissao>, IComissaoService
    {
        private readonly IComissaoRepository _repository;

        public ComissaoService(IComissaoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Comissao obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<Comissao> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissionalIdAndSatus(int profissionalId, Enums.Enum.StatusComissao status)
        {
            return await _repository.GetByProfissionalIdAndSatus(profissionalId, status);
        }

        public async Task<IEnumerable<Comissao>> GetByStatus(Enums.Enum.StatusComissao status)
        {
            return await _repository.GetByStatus(status);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndDate(int profissionalId, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await _repository.GetByProfissinalAndDate(profissionalId, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndPagamento(int profissionalId, DateTime dataPagamento)
        {
            return await _repository.GetByProfissinalAndPagamento(profissionalId, dataPagamento);
        }
    }
}
