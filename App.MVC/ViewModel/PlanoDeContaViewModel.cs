using System;
using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class PlanoDeContaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string Nome { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito Tipo { get; set; }

        [Display(Name = "Padrão da tela:")]
        public ControleDeSalao.Domain.Enums.Enum.TelaPadrao TelaPadrao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }
    }
}