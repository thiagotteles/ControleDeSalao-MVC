using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Mensalidade
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public DateTime DataVencimento { get; set; }

        public double Valor { get; set; }

        public DateTime? DataPagamento { get; set; }

        public double? ValorPago { get; set; }

        public string SacadoCPF { get; set; }

        public string SacadoNome { get; set; }

        public string ArquivoRetorno { get; set; }
    }
}
