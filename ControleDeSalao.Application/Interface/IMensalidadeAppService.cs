using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IMensalidadeAppService : IAppServiceBase<Mensalidade>
    {
        Task<int> Save(Mensalidade obj);
        Task<IEnumerable<Mensalidade>> GetAll();
        Task<IEnumerable<Mensalidade>> GetByEmpresaId(int empresaId);
        Task<Mensalidade> GetById(int id);
    }
}
