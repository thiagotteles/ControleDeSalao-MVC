using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.MVC.ViewModels
{
    public class ClienteViewModel
    {
        [Key]
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        [Required(ErrorMessage = ("Digite o Nome do Cliente"))]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        [Display(Name = "Incrição Estadual")]
        public string IE { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [NotMapped]
        [Display(Name = "Data de Nascimento")]
        public string SDataNascimento { get; set; }
        public DateTime? DataNascimento { get; set; }
        [Display(Name = "Vlr. Crédito")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double? ValorCredito { get; set; }
        [Display(Name = "Vlr. Débito")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public double? ValorDebito { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public bool Status { get; set; }
    }
}