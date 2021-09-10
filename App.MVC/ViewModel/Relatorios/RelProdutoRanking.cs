using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels.Relatorios
{
    public class RelProdutoRanking
    {
        public int ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public double Quantidade { get; set; }

        public string Unidade { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}