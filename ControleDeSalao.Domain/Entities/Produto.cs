using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string EAN { get; set; }
        public double? PrecoCusto { get; set; }
        public double? PrecoVenda { get; set; }
        public string Unidade { get; set; }
        public double? QtdEstoque { get; set; }
        public int? QtdMinEstoque { get; set; }
        public double? ValorParaProfissional { get; set; }
        public string Observacao { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
    }
}
