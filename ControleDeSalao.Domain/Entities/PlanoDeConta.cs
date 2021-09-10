using System;

namespace ControleDeSalao.Domain.Entities
{
    public class PlanoDeConta
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public Enums.Enum.TipoCreditoDebito Tipo { get; set; }
        public Enums.Enum.TelaPadrao TelaPadrao { get; set; }
        public string Observacao { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
    }
}
