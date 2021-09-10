using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class ComandaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int? ProfissionalId { get; set; }

        public int? ClienteId { get; set; }

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
        public double ValorTotal { get; set; }

        [DataType(DataType.Currency)]
        public double? ValorDesconto { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.StatusComanda Status { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        [NotMapped]
        public ClienteViewModel Cliente { get; set; }

        [NotMapped]
        public ProfissionalViewModel Profissional { get; set; }

        [NotMapped]
        public List<ServicoComandaViewModel> Servicos { get; set; }

        [NotMapped]
        public List<ProdutoComandaViewModel> Produtos { get; set; }

        [NotMapped]
        public string NomeCliente { get; set; }

        [NotMapped]
        public string NomeProfissional { get; set; }

        [NotMapped]
        public string NovoServico { get; set; }

        [NotMapped]
        public string NovoValor { get; set; }

        [NotMapped]
        [Display(Name = "hs")]
        public string NovaHoraDuracao { get; set; }

        [NotMapped]
        [Display(Name = "min")]
        public string NovoMinutoDuracao { get; set; }
        
        [NotMapped]
        [Display(Name = "% comissão Profissional")]
        public string NovaComissao { get; set; }

        [NotMapped]
        public string NovoProduto { get; set; }

        [NotMapped]
        public string NovaQtdProduto { get; set; }

        [NotMapped]
        public string NovoValorProduto { get; set; }
    }
}