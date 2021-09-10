using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Repositories
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<int> Save(Usuario obj);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByLogin(string login);
        Task<Usuario> GetByEmail(string email);
        string GetAllDireitos();
    }
}
