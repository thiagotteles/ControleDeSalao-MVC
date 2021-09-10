using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class EmpresaViewModel
    {
        [Key]
        [Display(Name = "Id. Empresa")]
        public int Id { get; set; }

        public int? PlanoId { get; set; }

        [Display(Name = "Nome da Empresa")]
        [Required(ErrorMessage = ("Digite o Nome da Empresa"))]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Endereco { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int PassoWizard { get; set; }

        [Display(Name = @"Nome do responsável")]
        public string NomeResponsavel { get; set; }

        [Display(Name = "CPF do responsável")]
        public string CPFResponsavel { get; set; }

        public DateTime DataBloqueio { get; set; }

        public DateTime DataAlerta { get; set; }

        [Range(1, 31, ErrorMessage = "Digite entre 1 e 31")]
        [Display(Name = "Dia de Vencimento")]
        public int? DiaParaVencimento { get; set; }

        public string CodigoPromocional { get; set; }

        [Display(Name = "SMS padrão de agendamento")]
        public string SmsAgenda { get; set; }

        [Display(Name = "SMS padrão de aniversário")]
        public string SmsAniversario { get; set; }

        public int QtdSMSBonus { get; set; }

        public int QtdSMSPago { get; set; }

        [NotMapped]
        public HorarioFuncionamentoViewModel HorarioFuncionamento { get; set; }

        [NotMapped]
        public IEnumerable<UsuarioViewModel> Usuarios { get; set; }

        [NotMapped]
        public int DiasRestante
        {
            get
            {
                return (int) DataBloqueio.Subtract(DateTime.Today).TotalDays;
            }
        }
    }
}