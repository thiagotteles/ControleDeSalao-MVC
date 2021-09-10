using ControleDeSalao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class CompraViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int FornecedorId { get; set; }

        [Display(Name = "Nº Nota")]
        public string NumeroNota { get; set; }

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

        public DateTime Data { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public double ValorTotal { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public ControleDeSalao.Domain.Enums.Enum.StatusCompra Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        [NotMapped]
        public string Parametros { get; set; }

        [NotMapped]
        public List<CompraDetalheViewModel> Detalhes { get; set; }

        [NotMapped]
        public FornecedorViewModel Fornecedor { get; set; }
    }
}