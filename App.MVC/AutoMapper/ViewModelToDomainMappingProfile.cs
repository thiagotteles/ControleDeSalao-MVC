using AutoMapper;
using ControleDeSalao.Domain.Entities;
using App.MVC.ViewModels;

namespace Panco.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Cliente, ClienteViewModel>();
            Mapper.CreateMap<Profissional, ProfissionalViewModel>();
            Mapper.CreateMap<Comissao, ComissaoViewModel>();
            Mapper.CreateMap<ProfissionalCategoria, ProfissionalCategoriaViewModel>();
            Mapper.CreateMap<Disponibilidade, DisponibilidadeViewModel>();
            Mapper.CreateMap<Categoria, CategoriaViewModel>();
            Mapper.CreateMap<PlanoDeConta, PlanoDeContaViewModel>();
            Mapper.CreateMap<Servico, ServicoViewModel>();
            Mapper.CreateMap<ComissaoPersonalizada, ComissaoPersonalizadaViewModel>();
            Mapper.CreateMap<ServicoPreCadastrado, ServicoPreCadastradoViewModel>();
            Mapper.CreateMap<Agenda, AgendaViewModel>();
            Mapper.CreateMap<Produto, ProdutoViewModel>();
            Mapper.CreateMap<Fornecedor, FornecedorViewModel>();
            Mapper.CreateMap<Compra, CompraViewModel>();
            Mapper.CreateMap<CompraDetalhe, CompraDetalheViewModel>();
            Mapper.CreateMap<CompraPagamento, CompraPagamentoViewModel>();
            Mapper.CreateMap<CompraPagamentoParcela, CompraPagamentoParcelaViewModel>();
            Mapper.CreateMap<Financeiro, FinanceiroViewModel>();
            Mapper.CreateMap<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>();
            Mapper.CreateMap<ComandaPagamento, ComandaPagamentoViewModel>();
            Mapper.CreateMap<Comanda, ComandaViewModel>();
            Mapper.CreateMap<ProdutoComanda, ProdutoComandaViewModel>();
            Mapper.CreateMap<ServicoComanda, ServicoComandaViewModel>();
            Mapper.CreateMap<Empresa, EmpresaViewModel>();
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Mensalidade, MensalidadeViewModel>();
            Mapper.CreateMap<HorarioFuncionamento, HorarioFuncionamentoViewModel>();
            Mapper.CreateMap<DisponibilidadeViewModel, HorarioFuncionamentoViewModel>();
        }
    }
}