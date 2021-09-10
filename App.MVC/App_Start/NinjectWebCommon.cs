using ControleDeSalao.Application.Interface;
using ControleDeSalao.Application.Service;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using ControleDeSalao.Domain.Services;
using ControleDeSalao.Infra.Data.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(App.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(App.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace App.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IClienteAppService>().To<ClienteAppService>();
            kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
            kernel.Bind<IEmpresaAppService>().To<EmpresaAppService>();
            kernel.Bind<IPromocaoAppService>().To<PromocaoAppService>();
            kernel.Bind<IPlanoDeContaAppService>().To<PlanoDeContaAppService>();
            kernel.Bind<IProfissionalAppService>().To<ProfissionalAppService>();
            kernel.Bind<IDisponibilidadeAppService>().To<DisponibilidadeAppService>();
            kernel.Bind<IProfissionalCategoriaAppService>().To<ProfissionalCategoriaAppService>();
            kernel.Bind<IComissaoAppService>().To<ComissaoAppService>();
            kernel.Bind<ICategoriaAppService>().To<CategoriaAppService>();
            kernel.Bind<IFinanceiroAppService>().To<FinanceiroAppService>();
            kernel.Bind<IServicoAppService>().To<ServicoAppService>();
            kernel.Bind<IComissaoPersonalizadaAppService>().To<ComissaoPersonalizadaAppService>();
            kernel.Bind<IServicoPreCadastradoAppService>().To<ServicoPreCadastradoAppService>();
            kernel.Bind<IAgendaAppService>().To<AgendaAppService>();
            kernel.Bind<IProdutoAppService>().To<ProdutoAppService>();
            kernel.Bind<IFornecedorAppService>().To<FornecedorAppService>();
            kernel.Bind<ICompraPagamentoParcelaAppService>().To<CompraPagamentoParcelaAppService>();
            kernel.Bind<ICompraPagamentoAppService>().To<CompraPagamentoAppService>();
            kernel.Bind<ICompraDetalheAppService>().To<CompraDetalheAppService>();
            kernel.Bind<ICompraAppService>().To<CompraAppService>();
            kernel.Bind<IComandaPagamentoParcelaAppService>().To<ComandaPagamentoParcelaAppService>();
            kernel.Bind<IComandaPagamentoAppService>().To<ComandaPagamentoAppService>();
            kernel.Bind<IComandaAppService>().To<ComandaAppService>();
            kernel.Bind<IServicoComandaAppService>().To<ServicoComandaAppService>();
            kernel.Bind<IProdutoComandaAppService>().To<ProdutoComandaAppService>();
            kernel.Bind<IMensalidadeAppService>().To<MensalidadeAppService>();
            kernel.Bind<IPlanoAppService>().To<PlanoAppService>();
            kernel.Bind<IHorarioFuncionamentoAppService>().To<HorarioFuncionamentoAppService>();
            kernel.Bind<IEmailMarketingAppService>().To<EmailMarketingAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IClienteService>().To<ClienteService>();
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<IEmpresaService>().To<EmpresaService>();
            kernel.Bind<IPromocaoService>().To<PromocaoService>();
            kernel.Bind<IPlanoDeContaService>().To<PlanoDeContaService>();
            kernel.Bind<IProfissionalService>().To<ProfissionalService>();
            kernel.Bind<IDisponibilidadeService>().To<DisponibilidadeService>();
            kernel.Bind<IProfissionalCategoriaService>().To<ProfissionalCategoriaService>();
            kernel.Bind<IComissaoService>().To<ComissaoService>();
            kernel.Bind<ICategoriaService>().To<CategoriaService>();
            kernel.Bind<IFinanceiroService>().To<FinanceiroService>();
            kernel.Bind<IServicoService>().To<ServicoService>();
            kernel.Bind<IComissaoPersonalizadaService>().To<ComissaoPersonalizadaService>();
            kernel.Bind<IServicoPreCadastradoService>().To<ServicoPreCadastradoService>();
            kernel.Bind<IAgendaService>().To<AgendaService>();
            kernel.Bind<IProdutoService>().To<ProdutoService>();
            kernel.Bind<IFornecedorService>().To<FornecedorService>();
            kernel.Bind<ICompraPagamentoParcelaService>().To<CompraPagamentoParcelaService>();
            kernel.Bind<ICompraPagamentoService>().To<CompraPagamentoService>();
            kernel.Bind<ICompraDetalheService>().To<CompraDetalheService>();
            kernel.Bind<ICompraService>().To<CompraService>();
            kernel.Bind<IComandaPagamentoParcelaService>().To<ComandaPagamentoParcelaService>();
            kernel.Bind<IComandaPagamentoService>().To<ComandaPagamentoService>();
            kernel.Bind<IComandaService>().To<ComandaService>();
            kernel.Bind<IServicoComandaService>().To<ServicoComandaService>();
            kernel.Bind<IProdutoComandaService>().To<ProdutoComandaService>();
            kernel.Bind<IMensalidadeService>().To<MensalidadeService>();
            kernel.Bind<IPlanoService>().To<PlanoService>();
            kernel.Bind<IHorarioFuncionamentoService>().To<HorarioFuncionamentoService>();
            kernel.Bind<IEmailMarketingService>().To<EmailMarketingService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>();
            kernel.Bind<IPromocaoRepository>().To<PromocaoRepository>();
            kernel.Bind<IPlanoDeContaRepository>().To<PlanoDeContaRepository>();
            kernel.Bind<IProfissionalRepository>().To<ProfissionalRepository>();
            kernel.Bind<IDisponibilidadeRepository>().To<DisponibilidadeRepository>();
            kernel.Bind<IProfissionalCategoriaRepository>().To<ProfissionalCategoriaRepository>();
            kernel.Bind<IComissaoRepository>().To<ComissaoRepository>();
            kernel.Bind<ICategoriaRepository>().To<CategoriaRepository>();
            kernel.Bind<IFinanceiroRepository>().To<FinanceiroRepository>();
            kernel.Bind<IServicoRepository>().To<ServicoRepository>();
            kernel.Bind<IComissaoPersonalizadaRepository>().To<ComissaoPersonalizadaRepository>();
            kernel.Bind<IServicoPreCadastradoRepository>().To<ServicoPreCadastradoRepository>();
            kernel.Bind<IAgendaRepository>().To<AgendaRepository>();
            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();
            kernel.Bind<IFornecedorRepository>().To<FornecedorRepository>();
            kernel.Bind<ICompraPagamentoParcelaRepository>().To<CompraPagamentoParcelaRepository>();
            kernel.Bind<ICompraPagamentoRepository>().To<CompraPagamentoRepository>();
            kernel.Bind<ICompraDetalheRepository>().To<CompraDetalheRepository>();
            kernel.Bind<ICompraRepository>().To<CompraRepository>();
            kernel.Bind<IComandaPagamentoParcelaRepository>().To<ComandaPagamentoParcelaRepository>();
            kernel.Bind<IComandaPagamentoRepository>().To<ComandaPagamentoRepository>();
            kernel.Bind<IComandaRepository>().To<ComandaRepository>();
            kernel.Bind<IServicoComandaRepository>().To<ServicoComandaRepository>();
            kernel.Bind<IProdutoComandaRepository>().To<ProdutoComandaRepository>();
            kernel.Bind<IMensalidadeRepository>().To<MensalidadeRepository>();
            kernel.Bind<IPlanoRepository>().To<PlanoRepository>();
            kernel.Bind<IHorarioFuncionamentoRepository>().To<HorarioFuncionamentoRepository>();
            kernel.Bind<IEmailMarketingRepository>().To<EmailMarketingRepository>();
        }        
    }
}
