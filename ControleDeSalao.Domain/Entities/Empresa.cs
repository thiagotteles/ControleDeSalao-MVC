using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public int? PlanoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Endereco { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int PassoWizard { get; set; }
        public string NomeResponsavel { get; set; }
        public string CPFResponsavel { get; set; }
        public DateTime DataBloqueio { get; set; }
        public DateTime DataAlerta { get; set; }
        public int? DiaParaVencimento { get; set; }
        public string CodigoPromocional { get; set; }
        public string SmsAgenda { get; set; }
        public string SmsAniversario { get; set; }
        public int QtdSMSBonus { get; set; }
        public int QtdSMSPago { get; set; }
        public int QtdProfissionaisPorPlano()
        {
            return 1000;
        }

    }

}
