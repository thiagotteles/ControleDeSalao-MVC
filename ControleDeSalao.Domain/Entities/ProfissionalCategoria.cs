using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class ProfissionalCategoria
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public int CategoriaId { get; set; }
    }
}
