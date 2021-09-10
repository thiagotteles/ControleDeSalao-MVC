using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public string NomePlano { get; set; }
        public double Valor { get; set; }
        public int QtdProfissionais { get; set; }
    }
}
