using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class CompraPagamentoParcelaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int CompraPagamentoId { get; set; }

        private string _sDataVencimento { get; set; }

        [NotMapped]
        [Display(Name = "Vencimento")]
        public string SDataVencimento
        {
            get
            {
                if (string.IsNullOrEmpty(_sDataVencimento))
                    _sDataVencimento = DataVencimento != DateTime.MinValue
                        ? DataVencimento.ToString("dd/MM/yyyy")
                        : DateTime.MinValue.ToString("dd/MM/yyyy");

                return _sDataVencimento;
            }
            set { _sDataVencimento = value; }
        }

        public DateTime DataVencimento { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.FormaDePagamento FormaDePagamento { get; set; }

        public int PlanoDeContaId { get; set; }

        public int FinanceiroId { get; set; }

        public virtual PlanoDeContaViewModel PlanoDeConta { get; set; }
    }
}