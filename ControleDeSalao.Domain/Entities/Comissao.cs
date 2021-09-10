using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Comissao
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int ProfissionalId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataLancamento { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataPagamento { get; set; }
        public Enums.Enum.StatusComissao Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Profissional Profissional { get; set; }
    }
}
