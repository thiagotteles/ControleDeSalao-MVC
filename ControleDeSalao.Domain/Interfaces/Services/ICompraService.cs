using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface ICompraService : IServiceBase<Compra>
    {
        Task<int> Save(Compra obj);
        Task<IEnumerable<Compra>> GetAll();
        Task<Compra> GetById(int id);
        Task<IEnumerable<Compra>> GetByStatus(Enums.Enum.StatusCompra status);
        Task<IEnumerable<Compra>> GetByFornecedorIdAndPeriodo(int fornecedorId, DateTime dataInicial, DateTime dataFinal);
    }
}
