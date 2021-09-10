using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Promocao
    {
        public int Id { get; set; }
        public string CodigoPromocional { get; set; }
        public int QtdDiasGratis { get; set; }
        public double ValorDesconto { get; set; }
        public bool Status { get; set; }
    }
}
