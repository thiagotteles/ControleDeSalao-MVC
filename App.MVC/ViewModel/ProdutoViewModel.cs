using System;
using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Insira a Descrição do produto")]
        public string Descricao { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = @"GTIN \ EAN")]
        public string EAN { get; set; }

        [Display(Name = "Preço de Custo")]
        [Required(ErrorMessage = "Insira o Preço de Custo")]
        [DataType(DataType.Currency)]
        public double? PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessage = "Insira o Preço de Venda")]
        [DataType(DataType.Currency)]
        public double? PrecoVenda { get; set; }

        [Required(ErrorMessage = "Insira a Unidade de Medida")]
        public string Unidade { get; set; }

        [Display(Name = "Qtd. Estoque")]
        public double? QtdEstoque { get; set; }

        [Display(Name = "Qtd. Min. Estoque")]
        public int? QtdMinEstoque { get; set; }

        [Display(Name = "% comissão Profissional")]
        [Range(0, 100, ErrorMessage = "Digite entre 0 e 100 %")]
        public double? ValorParaProfissional { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }
    }
}