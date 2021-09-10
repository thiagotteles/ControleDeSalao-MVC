using System;
using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface ICompraAppService : IAppServiceBase<Compra>
    {
        Task<int> Save(Compra obj);
        Task<IEnumerable<Compra>> GetAll();
        Task<Compra> GetById(int id);
        Task<IEnumerable<Compra>> GetByStatus(Domain.Enums.Enum.StatusCompra status);
        Task<IEnumerable<Compra>> GetByFornecedorIdAndPeriodo(int fornecedorId, DateTime dataInicial, DateTime dataFinal);
    }
}
