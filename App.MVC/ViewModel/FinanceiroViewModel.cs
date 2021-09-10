using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class FinanceiroViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string Descricao { get; set; }

        public int PlanoDeContaId { get; set; }

        public int? ClienteId { get; set; }

        public int? FornecedorId { get; set; }

        public int? ProfissionalId { get; set; }

        public int? ContaFixaId { get; set; }

        public string NomeQuem { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito Tipo { get; set; }

        private string _SDtVcto;

        [NotMapped]
        public string SDtVcto
        {
            get
            {
                if (string.IsNullOrEmpty(_SDtVcto))
                    _SDtVcto = DataVencimento.ToString("dd/MM/yyyy");

                return _SDtVcto;
            }
            set { _SDtVcto = value; }
        }

        private string _SDtPagto;

        [NotMapped]
        public string SDtPagto
        {
            get
            {
                if (string.IsNullOrEmpty(_SDtPagto))
                    _SDtPagto = DataPagamento.HasValue ? DataPagamento.Value.ToString("dd/MM/yyyy") : string.Empty;

                return _SDtPagto;
            }
            set { _SDtPagto = value; }
        }

        private string _SValor;

        [NotMapped]
        public string SValor
        {
            get
            {
                if (string.IsNullOrEmpty(_SValor))
                    _SValor = Valor.HasValue ? Valor.Value.ToString("0.00") : string.Empty;

                return _SValor;
            }
            set { _SValor = value; }
        }

        public DateTime DataVencimento { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public double? Valor { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.FormaDePagamento FormaDePagamento { get; set; }

        [NotMapped]
        public string SFormaDePagamento
        {
            get
            {
                switch (FormaDePagamento)
                {
                    default:
                        return "Dinheiro";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Crédito:
                        return "Crédito";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Débito:
                        return "Débito";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Cheque:
                        return "Cheque";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Depósito:
                        return "Depósito";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Boleto:
                        return "Boleto";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Outros:
                        return "Outros";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Promissória:
                        return "Promissória";
                    case ControleDeSalao.Domain.Enums.Enum.FormaDePagamento.Pacote:
                        return "Pacote";
                }
            }
        }

        public DateTime? DataPagamento { get; set; }

        [DataType(DataType.Currency)]
        public double? ValorPago { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.StatusFinanceiro Status { get; set; }

        [NotMapped]
        public ClienteViewModel Cliente { get; set; }

        [NotMapped]
        public PlanoDeContaViewModel PlanoDeConta { get; set; }

        [NotMapped]
        public FornecedorViewModel Fornecedor  { get; set; }

        [NotMapped]
        public bool IncluirComoPago { get; set; }

        [NotMapped]
        public int QtdMeses { get; set; }
    }
}