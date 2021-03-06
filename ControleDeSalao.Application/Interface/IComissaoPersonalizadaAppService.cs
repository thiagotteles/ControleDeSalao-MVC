using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IComissaoPersonalizadaAppService : IAppServiceBase<ComissaoPersonalizada>
    {
        Task<int> Save(ComissaoPersonalizada obj);
        Task<IEnumerable<ComissaoPersonalizada>> GetAll();
        Task<ComissaoPersonalizada> GetById(int id);
        Task<IEnumerable<ComissaoPersonalizada>> GetByProfissionalId(int profissionalId);
        Task<IEnumerable<ComissaoPersonalizada>> GetByServicoId(int servicoId);
        Task<ComissaoPersonalizada> GetByServicoIdAndProfissionalId(int servicoId, int profissionalId);
    }
}
