using System;
using System.Collections.Generic;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class ComissaoAppService : AppServiceBase<Comissao>, IComissaoAppService
    {
        private readonly IComissaoService _service;

        public ComissaoAppService(IComissaoService service)
            : base(service)
        {
            _service = service;
        }
        public async Task<int> Save(Comissao obj)
        {
            return await _service.Save(obj);
        }

        public async Task<Comissao> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissionalIdAndSatus(int profissionalId, Domain.Enums.Enum.StatusComissao status)
        {
            return await _service.GetByProfissionalIdAndSatus(profissionalId, status);
        }

        public async Task<IEnumerable<Comissao>> GetByStatus(Domain.Enums.Enum.StatusComissao status)
        {
            return await _service.GetByStatus(status);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndDate(int profissionalId, DateTime dataInicial, DateTime dataFinal)
        {
            return await _service.GetByProfissinalAndDate(profissionalId, dataInicial, dataFinal);
        }

        public async Task<IEnumerable<Comissao>> GetByProfissinalAndPagamento(int profissionalId, DateTime dataPagamento)
        {
            return await _service.GetByProfissinalAndPagamento(profissionalId, dataPagamento);
        }
    }
}
