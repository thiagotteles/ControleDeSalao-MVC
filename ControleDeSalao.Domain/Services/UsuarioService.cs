using System.Linq;
using System.Reflection;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Usuario obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Usuario> GetByLogin(string login)
        {
            return await _repository.GetByLogin(login);
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public string GetAllDireitos()
        {
            var direitos = string.Empty;
            const string separador = "|";
            var myType = typeof(Funcionalidade);
            var  myPropInfo =  myType.GetMembers().ToList().Where(m => m.MemberType == MemberTypes.Property);
            return myPropInfo.Aggregate(direitos, (current, info) => current + (info.Name + separador));
        }
    }
}
