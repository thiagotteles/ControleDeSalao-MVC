using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int FornecedorId { get; set; }
        public string NumeroNota { get; set; }
        public DateTime Data { get; set; }
        public double ValorTotal { get; set; }
        public string Observacao { get; set; }
        public Enums.Enum.StatusCompra Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
    }
}
