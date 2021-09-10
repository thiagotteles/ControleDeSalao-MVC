using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public async Task<int> Save(Usuario obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = obj.EmpresaId == 0 ? Cookies.IdEmpresaLogada.Value : obj.EmpresaId;
                obj.DtCadastro = DatetimeBrasil.HorarioDeBrasilia();
                Db.Usuarios.Add(obj);
            }
            else
            {
                var objDb = Db.Usuarios.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Login = obj.Login;
                    objDb.Email = obj.Email;
                    objDb.Password = obj.Password;
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.DtUltLogin = DatetimeBrasil.HorarioDeBrasilia();
                    objDb.IpUltLogin = obj.IpUltLogin;
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.Tutorial = obj.Tutorial;
                    objDb.Cargo = obj.Cargo;
                    objDb.UrlAvatar = obj.UrlAvatar;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await Db.Usuarios.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value).ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await Db.Usuarios.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Usuario> GetByLogin(string login)
        {
            return await Db.Usuarios.FirstOrDefaultAsync(m => m.Login == login);
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await Db.Usuarios.FirstOrDefaultAsync(m => m.Email == email);
        }
    }
}
