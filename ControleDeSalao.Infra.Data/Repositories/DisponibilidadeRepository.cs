using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class DisponibilidadeRepository : RepositoryBase<Disponibilidade>, IDisponibilidadeRepository
    {
        public async Task<int> Save(Disponibilidade obj)
        {
            if (obj.Id == 0)
            {
                Db.Disponibilidades.Add(obj);
            }
            else
            {
                var objDb = Db.Disponibilidades.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.DomingoTrabalha = obj.DomingoTrabalha;
                    objDb.DomingoHrExpInicial = obj.DomingoHrExpInicial;
                    objDb.DomingoMinExpInicial = obj.DomingoMinExpInicial;
                    objDb.DomingoHrExpFinal = obj.DomingoHrExpFinal;
                    objDb.DomingoMinExpFinal = obj.DomingoMinExpFinal;
                    objDb.DomingoHrAlmInicial = obj.DomingoHrAlmInicial;
                    objDb.DomingoMinAlmInicial = obj.DomingoMinAlmInicial;
                    objDb.DomingoHrAlmFinal = obj.DomingoHrAlmFinal;
                    objDb.DomingoMinAlmFinal = obj.DomingoMinAlmFinal;

                    objDb.SegundaTrabalha = obj.SegundaTrabalha;
                    objDb.SegundaHrExpInicial = obj.SegundaHrExpInicial;
                    objDb.SegundaMinExpInicial = obj.SegundaMinExpInicial;
                    objDb.SegundaHrExpFinal = obj.SegundaHrExpFinal;
                    objDb.SegundaMinExpFinal = obj.SegundaMinExpFinal;
                    objDb.SegundaHrAlmInicial = obj.SegundaHrAlmInicial;
                    objDb.SegundaMinAlmInicial = obj.SegundaMinAlmInicial;
                    objDb.SegundaHrAlmFinal = obj.SegundaHrAlmFinal;
                    objDb.SegundaMinAlmFinal = obj.SegundaMinAlmFinal;

                    objDb.TercaTrabalha = obj.TercaTrabalha;
                    objDb.TercaHrExpInicial = obj.TercaHrExpInicial;
                    objDb.TercaMinExpInicial = obj.TercaMinExpInicial;
                    objDb.TercaHrExpFinal = obj.TercaHrExpFinal;
                    objDb.TercaMinExpFinal = obj.TercaMinExpFinal;
                    objDb.TercaHrAlmInicial = obj.TercaHrAlmInicial;
                    objDb.TercaMinAlmInicial = obj.TercaMinAlmInicial;
                    objDb.TercaHrAlmFinal = obj.TercaHrAlmFinal;
                    objDb.TercaMinAlmFinal = obj.TercaMinAlmFinal;

                    objDb.QuartaTrabalha = obj.QuartaTrabalha;
                    objDb.QuartaHrExpInicial = obj.QuartaHrExpInicial;
                    objDb.QuartaMinExpInicial = obj.QuartaMinExpInicial;
                    objDb.QuartaHrExpFinal = obj.QuartaHrExpFinal;
                    objDb.QuartaMinExpFinal = obj.QuartaMinExpFinal;
                    objDb.QuartaHrAlmInicial = obj.QuartaHrAlmInicial;
                    objDb.QuartaMinAlmInicial = obj.QuartaMinAlmInicial;
                    objDb.QuartaHrAlmFinal = obj.QuartaHrAlmFinal;
                    objDb.QuartaMinAlmFinal = obj.QuartaMinAlmFinal;

                    objDb.QuintaTrabalha = obj.QuintaTrabalha;
                    objDb.QuintaHrExpInicial = obj.QuintaHrExpInicial;
                    objDb.QuintaMinExpInicial = obj.QuintaMinExpInicial;
                    objDb.QuintaHrExpFinal = obj.QuintaHrExpFinal;
                    objDb.QuintaMinExpFinal = obj.QuintaMinExpFinal;
                    objDb.QuintaHrAlmInicial = obj.QuintaHrAlmInicial;
                    objDb.QuintaMinAlmInicial = obj.QuintaMinAlmInicial;
                    objDb.QuintaHrAlmFinal = obj.QuintaHrAlmFinal;
                    objDb.QuintaMinAlmFinal = obj.QuintaMinAlmFinal;

                    objDb.SextaTrabalha = obj.SextaTrabalha;
                    objDb.SextaHrExpInicial = obj.SextaHrExpInicial;
                    objDb.SextaMinExpInicial = obj.SextaMinExpInicial;
                    objDb.SextaHrExpFinal = obj.SextaHrExpFinal;
                    objDb.SextaMinExpFinal = obj.SextaMinExpFinal;
                    objDb.SextaHrAlmInicial = obj.SextaHrAlmInicial;
                    objDb.SextaMinAlmInicial = obj.SextaMinAlmInicial;
                    objDb.SextaHrAlmFinal = obj.SextaHrAlmFinal;
                    objDb.SextaMinAlmFinal = obj.SextaMinAlmFinal;

                    objDb.SabadoTrabalha = obj.SabadoTrabalha;
                    objDb.SabadoHrExpInicial = obj.SabadoHrExpInicial;
                    objDb.SabadoMinExpInicial = obj.SabadoMinExpInicial;
                    objDb.SabadoHrExpFinal = obj.SabadoHrExpFinal;
                    objDb.SabadoMinExpFinal = obj.SabadoMinExpFinal;
                    objDb.SabadoHrAlmInicial = obj.SabadoHrAlmInicial;
                    objDb.SabadoMinAlmInicial = obj.SabadoMinAlmInicial;
                    objDb.SabadoHrAlmFinal = obj.SabadoHrAlmFinal;
                    objDb.SabadoMinAlmFinal = obj.SabadoMinAlmFinal;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<Disponibilidade> GetById(int id)
        {
            return await Db.Disponibilidades.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
