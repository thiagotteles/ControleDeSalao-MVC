using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        Task<int> Save(Usuario obj);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByLogin(string login);
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> IsValid(string login, string password);
        string GetAllDireitos();
    }
}
