using System;
using System.Collections.Generic;

namespace ControleDeSalao.Domain.Entities
{
    public class Servico
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Descricao { get; set; }
        public int? CategoriaId { get; set; }
        public int? ServicoPreCadastradoId { get; set; }
        public string Codigo { get; set; }
        public double? Valor { get; set; }
        public double? ValorParaProfissional { get; set; }
        public int? HoraDuracao { get; set; }
        public int? MinutoDuracao { get; set; }
        public string Observacao { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        //public virtual Categoria Categoria { get; set; }
    }
}
