using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class AgendaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int? EmpresaId { get; set; }

        public int? ProfissionalId { get; set; }

        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "Selecione um Serviço")]
        public int? ServicoId { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Selecione um Cliente")]
        public int? ClienteId { get; set; }

        public int? ComandaId { get; set; }

        public DateTime Data { get; set; }

        private string _sData { get; set; }

        [NotMapped]
        [Display(Name = "Data")]
        public string SData
        {
            get
            {
                if (string.IsNullOrEmpty(_sData))
                    _sData = Data != DateTime.MinValue
                        ? Data.ToString("dd/MM/yyyy")
                        : DateTime.MinValue.ToString("dd/MM/yyyy");

                return _sData;
            }
            set { _sData = value; }
        }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        public int HoraInicial { get; set; }

        public int HoraFinal { get; set; }

        private string _sHorario { get; set; }

        [NotMapped]
        [Display(Name = "Horas")]
        public string Horario
        {
            get
            {
                if (string.IsNullOrEmpty(_sHorario))
                    _sHorario = string.Format("{0}:{1}", HoraInicial.ToString("0000").Substring(0, 2), HoraInicial.ToString("0000").Substring(2, 2));

                return _sHorario;
            }
            set { _sHorario = value; }
        }

        [NotMapped]
        public string Duracao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public bool Status { get; set; }

        public bool? EnviarSMS { get; set; }

        public bool? EnviouSMS { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        [NotMapped]
        public int HoraDuracao { get; set; }

        [NotMapped]
        public int MinutoDuracao { get; set; }

        [NotMapped]
        public string Parametros { get; set; }

        [NotMapped]
        public string NomeServico { get; set; }

        [NotMapped]
        [Display(Name = "% comissão Profissional")]
        [Range(0, 100)]
        public int NovaComissao { get; set; }

        public virtual ServicoViewModel Servico { get; set; }

        public virtual ClienteViewModel Cliente { get; set; }

        public virtual ProfissionalViewModel Profissional { get; set; }

        [NotMapped]
        public string NomeCliente { get; set; }

        [NotMapped]
        [Display(Name = "Celular")]
        public string CelularCliente { get; set; }
    }
}