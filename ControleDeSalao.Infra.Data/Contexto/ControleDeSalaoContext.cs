using ControleDeSalao.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ControleDeSalao.Infra.Data.EntityConfig;

namespace ControleDeSalao.Infra.Data.Contexto
{
    public class ControleDeSalaoContext : DbContext
    {
        public ControleDeSalaoContext()
            : base("ControleDeSalaoContext")
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<PlanoDeConta> PlanoDeContas { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Disponibilidade> Disponibilidades { get; set; }
        public DbSet<ProfissionalCategoria> ProfissionalCategorias { get; set; }
        public DbSet<Comissao> Comissoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Financeiro> Financeiros { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ComissaoPersonalizada> ComissoesPersonalizadas { get; set; }
        public DbSet<ServicoPreCadastrado> ServicosPreCadastrados { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraDetalhe> ComprasDetalhes { get; set; }
        public DbSet<CompraPagamento> ComprasPagamentos { get; set; }
        public DbSet<CompraPagamentoParcela> ComprasPagamentosParcelas { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ServicoComanda> ServicosComanda { get; set; }
        public DbSet<ProdutoComanda> ProdutosComanda { get; set; }
        public DbSet<ComandaPagamento> ComandasPagamentos { get; set; }
        public DbSet<ComandaPagamentoParcela> ComandasPagamentosParcelas { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<HorarioFuncionamento> HorariosFuncionamentos { get; set; }
        public DbSet<EmailMarketing> EmailsMarketing { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ComissaoConfiguration());
        }
    }
}
