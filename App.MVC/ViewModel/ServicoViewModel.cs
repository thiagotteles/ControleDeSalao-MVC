using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace App.MVC.ViewModels
{
    public class ServicoViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Insira a Descrição do serviço")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public int? ServicoPreCadastradoId { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        public double? Valor { get; set; }

        [Display(Name = "% comissão Profissional")]
        public double? ValorParaProfissional { get; set; }

        [Display(Name = "hs")]
        [Required(ErrorMessage = "Insira a Hora de duração")]
        public int? HoraDuracao { get; set; }

        [Display(Name = "min")]
        [Required(ErrorMessage = "Insira o Minuto de duração")]
        public int? MinutoDuracao { get; set; }

        [NotMapped]
        [Display(Name = "Duração")]
        public string TempoDuracao
        {
            get
            {
                return (HoraDuracao.HasValue ? HoraDuracao.Value.ToString(CultureInfo.InvariantCulture) : "0") + ":" + (MinutoDuracao.HasValue ? MinutoDuracao.Value.ToString(CultureInfo.InvariantCulture) : "0");
            }
        }

        [NotMapped]
        public string Duracao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        [NotMapped]
        public List<ComissaoPersonalizadaViewModel> ComissoesPersonalizadas { get; set; }

        public virtual CategoriaViewModel Categoria { get; set; }
    }
}