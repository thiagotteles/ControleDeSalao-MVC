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
    public class ProfissionalCategoriaRepository : RepositoryBase<ProfissionalCategoria>, IProfissionalCategoriaRepository
    {
        public async Task<int> Save(ProfissionalCategoria obj)
        {
            if (obj.Id == 0)
            {
                Db.ProfissionalCategorias.Add(obj);
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<ProfissionalCategoria>> GetByProfissionalId(int profissionalId)
        {
            return await Db.ProfissionalCategorias.Where(m => (m.ProfissionalId == profissionalId || profissionalId == 0)).ToListAsync();
        }

        public async Task Remove(ProfissionalCategoria obj)
        {
            var objDb = await Db.ProfissionalCategorias.FirstOrDefaultAsync(m => m.CategoriaId == obj.CategoriaId && m.ProfissionalId == obj.ProfissionalId);
            Db.ProfissionalCategorias.Remove(objDb);
            await Db.SaveChangesAsync();
        }
    }
}
