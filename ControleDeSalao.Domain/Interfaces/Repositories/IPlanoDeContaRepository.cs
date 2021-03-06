using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IPlanoDeContaRepository : IRepositoryBase<PlanoDeConta>
    {
        Task<int> Save(PlanoDeConta obj);
        Task<IEnumerable<PlanoDeConta>> GetAll(bool withInativo = false);
        Task<PlanoDeConta> GetById(int id, bool withInativo = false);
        Task<PlanoDeConta> GetByTelaPadrao(Enums.Enum.TelaPadrao telaPadrao, bool withInativo = false);
        Task<IEnumerable<PlanoDeConta>> GetByTipo(Enums.Enum.TipoCreditoDebito tipo, bool withInativo = false);
    }
}
