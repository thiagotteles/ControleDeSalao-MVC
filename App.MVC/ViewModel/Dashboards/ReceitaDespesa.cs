using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.MVC.ViewModel.Dashboards
{
    public class ReceitaDespesa
    {
        public string mes { get; set; }
        public double receita { get; set; }
        public double despesa { get; set; }
        public int dashLengthLine { get; set; }
        public int dashLengthColumn { get; set; }
        public double alpha { get; set; }
        public string additional { get; set; }
    }
}