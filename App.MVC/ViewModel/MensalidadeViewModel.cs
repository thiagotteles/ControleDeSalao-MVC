using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class MensalidadeViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public DateTime DataVencimento { get; set; }

        [NotMapped]
        public string SDataVencimento
        {
            get
            {
                return DataVencimento.ToString("d");
            }
        }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        public DateTime? DataPagamento { get; set; }

        [NotMapped]
        public string SDataPagamento
        {
            get
            {
                return DataPagamento.HasValue ? DataPagamento.Value.ToString("d") : string.Empty;
            }
        }

        public double? ValorPago { get; set; }

        public string SacadoCPF { get; set; }

        public string SacadoNome { get; set; }

        public string ArquivoRetorno { get; set; }

        [NotMapped]
        public EmpresaViewModel Empresa { get; set; }
    }
}