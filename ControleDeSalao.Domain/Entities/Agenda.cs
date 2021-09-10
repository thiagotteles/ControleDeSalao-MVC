using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Agenda
    {
        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? ProfissionalId { get; set; }
        public int? ServicoId { get; set; }
        public int? ClienteId { get; set; }
        public int? ComandaId { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public string Observacao { get; set; }
        public bool Status { get; set; }
        public bool? EnviarSMS { get; set; }
        public bool? EnviouSMS { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Profissional Profissional { get; set; }
    }
}
