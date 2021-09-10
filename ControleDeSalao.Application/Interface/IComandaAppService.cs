using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IComandaAppService : IAppServiceBase<Comanda>
    {
        Task<int> Save(Comanda obj);
        Task<IEnumerable<Comanda>> GetAll();
        Task<Comanda> GetById(int id);
        Task<IEnumerable<Comanda>> GetByProfissionalID(int profissionalID);
        Task<IEnumerable<Comanda>> GetByStatusAndData(Domain.Enums.Enum.StatusComanda status, DateTime? data);
        Task<IEnumerable<Comanda>> GetByStatusAndPeriodo(Domain.Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetByClienteIdStatusAndPeriodo(int clienteId, Domain.Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetByProfissionalIdStatusAndPeriodo(int profissionalId, Domain.Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetDescontoByStatusAndPeriodo(Domain.Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
    }
}
