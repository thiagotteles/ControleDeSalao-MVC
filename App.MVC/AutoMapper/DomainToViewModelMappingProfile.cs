using AutoMapper;
using ControleDeSalao.Domain.Entities;
using App.MVC.ViewModels;

namespace Panco.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ClienteViewModel, Cliente>();
            Mapper.CreateMap<ProfissionalViewModel, Profissional>();
            Mapper.CreateMap<ComissaoViewModel, Comissao>();
            Mapper.CreateMap<ProfissionalCategoriaViewModel, ProfissionalCategoria>();
            Mapper.CreateMap<DisponibilidadeViewModel, Disponibilidade>();
            Mapper.CreateMap<CategoriaViewModel, Categoria>();
            Mapper.CreateMap<PlanoDeContaViewModel, PlanoDeConta>();
            Mapper.CreateMap<ServicoViewModel, Servico>();
            Mapper.CreateMap<ComissaoPersonalizadaViewModel, ComissaoPersonalizada>();
            Mapper.CreateMap<ServicoPreCadastradoViewModel, ServicoPreCadastrado>();
            Mapper.CreateMap<AgendaViewModel, Agenda>();
            Mapper.CreateMap<ProdutoViewModel, Produto>();
            Mapper.CreateMap<FornecedorViewModel, Fornecedor>();
            Mapper.CreateMap<CompraViewModel, Compra>();
            Mapper.CreateMap<CompraDetalheViewModel, CompraDetalhe>();
            Mapper.CreateMap<CompraPagamentoViewModel, CompraPagamento>();
            Mapper.CreateMap<CompraPagamentoParcelaViewModel, CompraPagamentoParcela>();
            Mapper.CreateMap<FinanceiroViewModel, Financeiro>();
            Mapper.CreateMap<ComandaPagamentoParcelaViewModel, ComandaPagamentoParcela>();
            Mapper.CreateMap<ComandaPagamentoViewModel, ComandaPagamento>();
            Mapper.CreateMap<ComandaViewModel, Comanda>();
            Mapper.CreateMap<ServicoComandaViewModel, ServicoComanda>();
            Mapper.CreateMap<ProdutoComandaViewModel, ProdutoComanda>();
            Mapper.CreateMap<EmpresaViewModel, Empresa>();
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<MensalidadeViewModel, Mensalidade>();
            Mapper.CreateMap<HorarioFuncionamentoViewModel, HorarioFuncionamento>();
            Mapper.CreateMap<HorarioFuncionamentoViewModel, DisponibilidadeViewModel>();
        }
    }
}