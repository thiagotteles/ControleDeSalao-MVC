using ControleDeSalao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IComandaRepository : IRepositoryBase<Comanda>
    {
        Task<int> Save(Comanda obj);
        Task<IEnumerable<Comanda>> GetAll();
        Task<Comanda> GetById(int id);
        Task<IEnumerable<Comanda>> GetByProfissionalID(int profissionalID);
        Task<IEnumerable<Comanda>> GetByStatusAndData(Enums.Enum.StatusComanda status, DateTime? data);
        Task<IEnumerable<Comanda>> GetByStatusAndPeriodo(Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetByClienteIdStatusAndPeriodo(int clienteId, Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetByProfissionalIdStatusAndPeriodo(int profissionalId, Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Comanda>> GetDescontoByStatusAndPeriodo(Enums.Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal);
    }
}
