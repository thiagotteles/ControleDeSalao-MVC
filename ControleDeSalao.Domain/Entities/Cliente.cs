using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double? ValorCredito { get; set; }
        public double? ValorDebito { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public bool Status { get; set; }
    }
}
