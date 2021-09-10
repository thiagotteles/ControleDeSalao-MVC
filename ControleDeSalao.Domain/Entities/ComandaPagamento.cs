using System;

namespace ControleDeSalao.Domain.Entities
{
    public class ComandaPagamento
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int ComandaId { get; set; }
        public double Valor { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Comanda Comanda { get; set; }
    }
}
