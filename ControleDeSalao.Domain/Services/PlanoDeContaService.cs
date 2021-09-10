using System.Collections.Generic;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Enums;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class PlanoDeContaService : ServiceBase<PlanoDeConta>, IPlanoDeContaService
    {
        private readonly IPlanoDeContaRepository _repository;

        #region _listaDefault

        private List<PlanoDeConta> _listaDefault = new List<PlanoDeConta>
        {
            new PlanoDeConta
            {
                Nome = "Vendas",
                Tipo = Enum.TipoCreditoDebito.Crédito,
                TelaPadrao = Enum.TelaPadrao.Comandas,
            },
            new PlanoDeConta
            {
                Nome = "Produtos",
                Tipo = Enum.TipoCreditoDebito.Crédito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Serviços",
                Tipo = Enum.TipoCreditoDebito.Crédito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Água/Luz",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Aluguel",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Comissão",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Comissão,
            },
            new PlanoDeConta
            {
                Nome = "Salário",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Salario,
            },
            new PlanoDeConta
            {
                Nome = "Fornecedor",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Telefone/Internet",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Vales",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Impostos",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
            new PlanoDeConta
            {
                Nome = "Estoque",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Compras,
            },
            new PlanoDeConta
            {
                Nome = "Pro Labore",
                Tipo = Enum.TipoCreditoDebito.Débito,
                TelaPadrao = Enum.TelaPadrao.Nenhuma,
            },
        };

        #endregion

        public PlanoDeContaService(IPlanoDeContaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> Save(PlanoDeConta obj)
        {
           return await _repository.Save(obj);
        }

        public async Task<IEnumerable<PlanoDeConta>> GetAll(bool withInativo = false)
        {
            return await _repository.GetAll(withInativo);
        }

        public async Task<PlanoDeConta> GetById(int id, bool withInativo = false)
        {
            return await _repository.GetById(id, withInativo);
        }

        public async Task<PlanoDeConta> GetByTelaPadrao(Enum.TelaPadrao telaPadrao, bool withInativo = false)
        {
            return await _repository.GetByTelaPadrao(telaPadrao, withInativo);
        }

        public async Task<IEnumerable<PlanoDeConta>> GetByTipo(Enum.TipoCreditoDebito tipo, bool withInativo = false)
        {
            return await _repository.GetByTipo(tipo, withInativo);
        }

        public async Task AddAllDefaults(int empresaId, int usuarioId)
        {
            foreach (var planoDeConta in _listaDefault)
            {
                planoDeConta.EmpresaId = empresaId;
                planoDeConta.IdUsuarioCadastro = usuarioId;
                await _repository.Save(planoDeConta);
            }
        }
    }
}
