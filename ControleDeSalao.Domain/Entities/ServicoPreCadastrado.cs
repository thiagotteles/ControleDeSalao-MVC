using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class ServicoPreCadastrado
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
    }
}
