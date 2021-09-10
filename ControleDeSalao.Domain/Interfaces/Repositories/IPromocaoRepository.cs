using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IPromocaoRepository : IRepositoryBase<Promocao>
    {
        Task<Promocao> GetByCodigoPromocional(string codigoPromocional);
    }
}
