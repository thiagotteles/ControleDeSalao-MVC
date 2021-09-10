using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ControleDeSalao.Domain.Entities;

namespace App.MVC.ViewModels
{
    public class ProfissionalViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int UsuarioId { get; set; }

        public int DisponibilidadeId { get; set; }

        [Required(ErrorMessage = ("Digite o Nome do profissional"))]
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Endereco { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [NotMapped]
        [Display(Name = "Data de Contratação")]
        public string SDataContratacao { get; set; }

        public DateTime? DataContratacao { get; set; }

        [Display(Name = "% Comissão")]
        [Range(0, 100)]
        public int? Comissao { get; set; }

        [Display(Name = "Salário")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public double? Salario { get; set; }

        [Display(Name = "Utiliza Agenda?")]
        public bool GerarAgenda { get; set; }

        public string FotoPerfil { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        public bool Status { get; set; }

        [NotMapped]
        public UsuarioViewModel Usuario { get; set; }
        
        //public virtual DisponibilidadeViewModel Disponibilidade { get; set; }

        [NotMapped]
        public virtual List<ComissaoViewModel> Comissoes { get; set; }

        //[NotMapped]
        //public List<ProfissionalCategoriaViewModel> Categorias { get; set; }

        [NotMapped]
        public int PlanoDeContaId { get; set; }

        [NotMapped]
        public ControleDeSalao.Domain.Enums.Enum.FormaDePagamento FormaDePagamento { get; set; }

        [NotMapped]
        public string SDataPagamento { get; set; }

        [NotMapped]
        public List<AgendaViewModel> HorarioMarcado { get; set; }
    }
}