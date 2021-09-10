using System;

namespace ControleDeSalao.Domain.Entities
{
    public class ComissaoPersonalizada
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int ServicoId { get; set; }
        public int ProfissionalId { get; set; }
        public double Comissao { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Profissional Profissional { get; set; }
        public virtual Servico Servico { get; set; }
    }
}
