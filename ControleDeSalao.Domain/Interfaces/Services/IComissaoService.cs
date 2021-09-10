using ControleDeSalao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IComissaoService : IServiceBase<Comissao>
    {
        Task<int> Save(Comissao obj);
        Task<Comissao> GetById(int id);
        Task<IEnumerable<Comissao>> GetByProfissionalIdAndSatus(int profissionalId, Enums.Enum.StatusComissao status);
        Task<IEnumerable<Comissao>> GetByStatus(Enums.Enum.StatusComissao status);
        Task<IEnumerable<Comissao>> GetByProfissinalAndDate(int profissionalId, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comissao>> GetByProfissinalAndPagamento(int profissionalId, DateTime dataPagamento);
    }
}
