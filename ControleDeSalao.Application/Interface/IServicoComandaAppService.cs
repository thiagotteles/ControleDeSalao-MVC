using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IServicoComandaAppService : IAppServiceBase<ServicoComanda>
    {
        Task<int> Save(ServicoComanda obj);
        Task<ServicoComanda> GetById(int id);
        Task<IEnumerable<ServicoComanda>> GetByComandaId(int comandaId);
        Task<int> Remove(ServicoComanda obj);
        Task<IEnumerable<ServicoComanda>> GetAll();
        Task<IEnumerable<ServicoComanda>> GetByYear(int year);
        Task<IEnumerable<ServicoComanda>> GetByMonth(int year, int month);

    }
}
