using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Profissional
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public int DisponibilidadeId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DataContratacao { get; set; }
        public int? Comissao { get; set; }
        public double? Salario { get; set; }
        public bool GerarAgenda { get; set; }
        public string FotoPerfil { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public bool Status { get; set; }
        //public virtual Disponibilidade Disponibilidade { get; set; }
    }
}
