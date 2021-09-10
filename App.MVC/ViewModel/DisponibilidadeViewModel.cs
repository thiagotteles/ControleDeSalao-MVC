using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class DisponibilidadeViewModel
    {
        [Key]
        public int Id { get; set; }

        public bool DomingoTrabalha { get; set; }
        public int DomingoHrExpInicial { get; set; }
        public int DomingoMinExpInicial { get; set; }
        public int DomingoHrExpFinal { get; set; }
        public int DomingoMinExpFinal { get; set; }
        public int DomingoHrAlmInicial { get; set; }
        public int DomingoMinAlmInicial { get; set; }
        public int DomingoHrAlmFinal { get; set; }
        public int DomingoMinAlmFinal { get; set; }

        public bool SegundaTrabalha { get; set; }
        public int SegundaHrExpInicial { get; set; }
        public int SegundaMinExpInicial { get; set; }
        public int SegundaHrExpFinal { get; set; }
        public int SegundaMinExpFinal { get; set; }
        public int SegundaHrAlmInicial { get; set; }
        public int SegundaMinAlmInicial { get; set; }
        public int SegundaHrAlmFinal { get; set; }
        public int SegundaMinAlmFinal { get; set; }

        public bool TercaTrabalha { get; set; }
        public int TercaHrExpInicial { get; set; }
        public int TercaMinExpInicial { get; set; }
        public int TercaHrExpFinal { get; set; }
        public int TercaMinExpFinal { get; set; }
        public int TercaHrAlmInicial { get; set; }
        public int TercaMinAlmInicial { get; set; }
        public int TercaHrAlmFinal { get; set; }
        public int TercaMinAlmFinal { get; set; }

        public bool QuartaTrabalha { get; set; }
        public int QuartaHrExpInicial { get; set; }
        public int QuartaMinExpInicial { get; set; }
        public int QuartaHrExpFinal { get; set; }
        public int QuartaMinExpFinal { get; set; }
        public int QuartaHrAlmInicial { get; set; }
        public int QuartaMinAlmInicial { get; set; }
        public int QuartaHrAlmFinal { get; set; }
        public int QuartaMinAlmFinal { get; set; }

        public bool QuintaTrabalha { get; set; }
        public int QuintaHrExpInicial { get; set; }
        public int QuintaMinExpInicial { get; set; }
        public int QuintaHrExpFinal { get; set; }
        public int QuintaMinExpFinal { get; set; }
        public int QuintaHrAlmInicial { get; set; }
        public int QuintaMinAlmInicial { get; set; }
        public int QuintaHrAlmFinal { get; set; }
        public int QuintaMinAlmFinal { get; set; }

        public bool SextaTrabalha { get; set; }
        public int SextaHrExpInicial { get; set; }
        public int SextaMinExpInicial { get; set; }
        public int SextaHrExpFinal { get; set; }
        public int SextaMinExpFinal { get; set; }
        public int SextaHrAlmInicial { get; set; }
        public int SextaMinAlmInicial { get; set; }
        public int SextaHrAlmFinal { get; set; }
        public int SextaMinAlmFinal { get; set; }

        public bool SabadoTrabalha { get; set; }
        public int SabadoHrExpInicial { get; set; }
        public int SabadoMinExpInicial { get; set; }
        public int SabadoHrExpFinal { get; set; }
        public int SabadoMinExpFinal { get; set; }
        public int SabadoHrAlmInicial { get; set; }
        public int SabadoMinAlmInicial { get; set; }
        public int SabadoHrAlmFinal { get; set; }
        public int SabadoMinAlmFinal { get; set; }
    }
}