using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class ComissaoViewModel
    {
        [Key]
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int ProfissionalId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataLancamento { get; set; }
        public string SDataLancamento
        {
            get
            {
                return DataLancamento.ToString("dd/MM/yyyy");
            }
        }
        [DataType(DataType.Currency)]
        public double? Valor { get; set; }
        public DateTime? DataPagamento { get; set; }
        public ControleDeSalao.Domain.Enums.Enum.StatusComissao Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        [NotMapped]
        public bool Selecionada { get; set; }
        public virtual ProfissionalViewModel Profissional { get; set; }
    }
}