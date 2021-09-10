using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels.Relatorios
{
    public class RelProfissionalRanking
    {
        public string NomeProfissional { get; set; }

        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}