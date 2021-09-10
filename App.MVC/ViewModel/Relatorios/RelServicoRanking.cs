using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels.Relatorios
{
    public class RelServicoRanking
    {
        public int ServicoId { get; set; }

        public string NomeServico { get; set; }

        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}