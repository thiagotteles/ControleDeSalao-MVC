using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IPromocaoAppService : IAppServiceBase<Promocao>
    {
        Task<Promocao> GetByCodigoPromocional(string codigoPromocional);
    }
}
