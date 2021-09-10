using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class DisponibilidadeService : ServiceBase<Disponibilidade>, IDisponibilidadeService
    {
        private readonly IDisponibilidadeRepository _repository;

        public DisponibilidadeService(IDisponibilidadeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Disponibilidade obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<Disponibilidade> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public Disponibilidade GetAllDefaults()
        {
            var ret = new Disponibilidade();
            ret.DomingoTrabalha = false;
            ret.DomingoHrExpInicial = 8;
            ret.DomingoMinExpInicial = 0;
            ret.DomingoHrExpFinal = 20;
            ret.DomingoMinExpFinal = 0;
            ret.DomingoHrAlmInicial = 12;
            ret.DomingoMinAlmInicial = 0;
            ret.DomingoHrAlmFinal = 13;
            ret.DomingoMinAlmFinal = 0;

            ret.SegundaTrabalha = false;
            ret.SegundaHrExpInicial = 8;
            ret.SegundaMinExpInicial = 0;
            ret.SegundaHrExpFinal = 20;
            ret.SegundaMinExpFinal = 0;
            ret.SegundaHrAlmInicial = 12;
            ret.SegundaMinAlmInicial = 0;
            ret.SegundaHrAlmFinal = 13;
            ret.SegundaMinAlmFinal = 0;

            ret.TercaTrabalha = true;
            ret.TercaHrExpInicial = 8;
            ret.TercaMinExpInicial = 0;
            ret.TercaHrExpFinal = 20;
            ret.TercaMinExpFinal = 0;
            ret.TercaHrAlmInicial = 12;
            ret.TercaMinAlmInicial = 0;
            ret.TercaHrAlmFinal = 13;
            ret.TercaMinAlmFinal = 0;

            ret.QuartaTrabalha = true;
            ret.QuartaHrExpInicial = 8;
            ret.QuartaMinExpInicial = 0;
            ret.QuartaHrExpFinal = 20;
            ret.QuartaMinExpFinal = 0;
            ret.QuartaHrAlmInicial = 12;
            ret.QuartaMinAlmInicial = 0;
            ret.QuartaHrAlmFinal = 13;
            ret.QuartaMinAlmFinal = 0;

            ret.QuintaTrabalha = true;
            ret.QuintaHrExpInicial = 8;
            ret.QuintaMinExpInicial = 0;
            ret.QuintaHrExpFinal = 20;
            ret.QuintaMinExpFinal = 0;
            ret.QuintaHrAlmInicial = 12;
            ret.QuintaMinAlmInicial = 0;
            ret.QuintaHrAlmFinal = 13;
            ret.QuintaMinAlmFinal = 0;

            ret.SextaTrabalha = true;
            ret.SextaHrExpInicial = 8;
            ret.SextaMinExpInicial = 0;
            ret.SextaHrExpFinal = 20;
            ret.SextaMinExpFinal = 0;
            ret.SextaHrAlmInicial = 12;
            ret.SextaMinAlmInicial = 0;
            ret.SextaHrAlmFinal = 13;
            ret.SextaMinAlmFinal = 0;

            ret.SabadoTrabalha = true;
            ret.SabadoHrExpInicial = 8;
            ret.SabadoMinExpInicial = 0;
            ret.SabadoHrExpFinal = 20;
            ret.SabadoMinExpFinal = 0;
            ret.SabadoHrAlmInicial = 12;
            ret.SabadoMinAlmInicial = 0;
            ret.SabadoHrAlmFinal = 13;
            ret.SabadoMinAlmFinal = 0;
            return ret;
        }
    }
}
