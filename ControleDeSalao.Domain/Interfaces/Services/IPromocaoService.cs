using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IPromocaoService : IServiceBase<Promocao>
    {
        Task<Promocao> GetByCodigoPromocional(string codigoPromocional);
    }
}
