using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IPlanoDeContaAppService : IAppServiceBase<PlanoDeConta>
    {
        Task<int> Save(PlanoDeConta obj);
        Task<IEnumerable<PlanoDeConta>> GetAll(bool withInativo = false);
        Task<PlanoDeConta> GetById(int id, bool withInativo = false);
        Task<PlanoDeConta> GetByTelaPadrao(Domain.Enums.Enum.TelaPadrao telaPadrao, bool withInativo = false);
        Task<IEnumerable<PlanoDeConta>> GetByTipo(Domain.Enums.Enum.TipoCreditoDebito tipo, bool withInativo = false);
        Task AddAllDefaults(int empresaId, int usuarioId);
    }
}
