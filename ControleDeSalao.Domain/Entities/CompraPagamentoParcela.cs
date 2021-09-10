using System;

namespace ControleDeSalao.Domain.Entities
{
    public class CompraPagamentoParcela
    {
        public int Id { get; set; }
        public int CompraPagamentoId { get; set; }
        public DateTime DataVencimento { get; set; }
        public double Valor { get; set; }
        public Enums.Enum.FormaDePagamento FormaDePagamento { get; set; }
        public int PlanoDeContaId { get; set; }
        public int FinanceiroId { get; set; }
        public virtual PlanoDeConta PlanoDeConta { get; set; }
    }
}
