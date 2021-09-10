using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class CompraPagamentoViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int CompraId { get; set; }

        public double Valor { get; set; }

        public int QtdParcelas { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public List<CompraPagamentoParcelaViewModel> Parcelas { get; set; }

        [NotMapped]
        public CompraViewModel Compra { get; set; }

    }
}