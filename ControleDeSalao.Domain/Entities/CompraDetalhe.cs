
namespace ControleDeSalao.Domain.Entities
{
    public class CompraDetalhe
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public double Quantidade { get; set; }
        public double ValorProduto { get; set; }
        public double ValorTotal { get; set; }
    }
}
