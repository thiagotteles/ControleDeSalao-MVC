using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class ServicoService : ServiceBase<Servico>, IServicoService
    {
        #region _listaDefault

        private readonly List<Servico> _listaDefault = new List<Servico>
        {
            new Servico
            {
                Descricao = "Corte Masculino",
                Valor = 0,
                HoraDuracao = 0,
                MinutoDuracao = 0               
            },
            new Servico
            {
                Descricao = "Corte Feminino",
                Valor = 0,
                HoraDuracao = 0,
                MinutoDuracao = 0               
            },
            new Servico
            {
                Descricao = "Escova",
                Valor = 0,
                HoraDuracao = 0,
                MinutoDuracao = 0               
            },
            new Servico
            {
                Descricao = "Pé & Mão",
                Valor = 0,
                HoraDuracao = 0,
                MinutoDuracao = 0               
            },
            new Servico
            {
                Descricao = "Design de Sobrancelha",
                Valor = 0,
                HoraDuracao = 0,
                MinutoDuracao = 0               
            },
        };

        #endregion


        private readonly IServicoRepository _repository;

        public ServicoService(IServicoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(Servico obj)
        {
            return await _repository.Save(obj);
        }

        public async Task<IEnumerable<Servico>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Servico>> AutoComplete(string nome)
        {
            return await _repository.AutoComplete(nome);
        }

        public async Task<Servico> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Servico> GetByNome(string nome)
        {
            return await _repository.GetByNome(nome);
        }

        public async Task<IEnumerable<Servico>> GetByCategorias(int[] categoriasId)
        {
            return await _repository.GetByCategorias(categoriasId);
        }

        public async Task AddAllDefaults(int empresaId, int usuarioId)
        {
            foreach (var servico in _listaDefault)
            {
                servico.EmpresaId = empresaId;
                servico.IdUsuarioCadastro = usuarioId;
                await _repository.Save(servico);
            }
        }
    }
}
