using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Enums;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ControleDeSalao.Infra.Cross.Security;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ServicoComandaRepository : RepositoryBase<ServicoComanda>, IServicoComandaRepository
    {
        public async Task<int> Save(ServicoComanda obj)
        {
            if (obj.Id == 0)
            {
                Db.ServicosComanda.Add(obj);
            }
            else
            {
                var objDb = Db.ServicosComanda.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.ComandaId = obj.ComandaId;
                    objDb.ServicoId = obj.ServicoId;
                    objDb.Valor = obj.Valor;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<ServicoComanda> GetById(int id)
        {
            return await Db.ServicosComanda.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<ServicoComanda>> GetByComandaId(int comandaId)
        {
            return await Db.ServicosComanda.Where(m => m.ComandaId == comandaId).ToListAsync();
        }

        public async Task<int> Remove(ServicoComanda obj)
        {
            var objDb = Db.ServicosComanda.Find(obj.Id);
            if (objDb != null)
            {
                Db.ServicosComanda.Remove(objDb);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<ServicoComanda>> GetAll()
        {
            var comandas =
                 Db.Comandas.Where(
                     m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusComanda.Excluida)
                     .Select(m => m.Id)
                     .ToArray();

            return await Db.ServicosComanda.Where(m => comandas.Contains(m.ComandaId)).ToListAsync();
        }


        public async Task<IEnumerable<ServicoComanda>> GetByYear(int year)
        {
            var comandas =
                 Db.Comandas.Where(
                     m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Data.Year == year && m.Status != Enum.StatusComanda.Excluida)
                     .Select(m => m.Id)
                     .ToArray();

            return await Db.ServicosComanda.Where(m => comandas.Contains(m.ComandaId)).ToListAsync();
        }


        public async Task<IEnumerable<ServicoComanda>> GetByMonth(int year, int month)
        {
            var comandas =
             Db.Comandas.Where(
                 m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Data.Year == year && m.Data.Month == month && m.Status != Enum.StatusComanda.Excluida)
                 .Select(m => m.Id)
                 .ToArray();

            return await Db.ServicosComanda.Where(m => comandas.Contains(m.ComandaId)).ToListAsync();
        }
    }
}
