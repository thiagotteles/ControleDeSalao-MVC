using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Domain.Entities
{
    public class Financeiro
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string Descricao { get; set; }

        public int PlanoDeContaId { get; set; }

        public int? ClienteId { get; set; }

        public int? FornecedorId { get; set; }

        public int? ProfissionalId { get; set; }

        public int? ContaFixaId { get; set; }

        public string NomeQuem { get; set; }

        public Enum.TipoCreditoDebito Tipo { get; set; }

        public DateTime DataVencimento { get; set; }

        public double? Valor { get; set; }

        public Enum.FormaDePagamento FormaDePagamento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public double? ValorPago { get; set; }

        public Enum.StatusFinanceiro Status { get; set; }
    }
}
