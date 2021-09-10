using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Comanda
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int? ProfissionalId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime Data { get; set; }
        public double ValorTotal { get; set; }
        public double? ValorDesconto { get; set; }
        public Enums.Enum.StatusComanda Status { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
    }
}
