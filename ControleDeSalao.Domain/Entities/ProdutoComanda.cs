
namespace ControleDeSalao.Domain.Entities
{
    public class ProdutoComanda
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public double Quantidade { get; set; }
        public double ValorProduto { get; set; }
        public double ValorTotal { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
