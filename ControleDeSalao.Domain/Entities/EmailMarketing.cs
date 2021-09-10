
using System;

namespace ControleDeSalao.Domain.Entities
{
    public class EmailMarketing
    {
        public int Id { get; set; }
        public string Endereco { get; set; }
        public bool Enviado { get; set; }
        public bool Visualizou { get; set; }
        public string UrlRedirect { get; set; }
        public DateTime? DataDeVisualizacao { get; set; }
    }
}
