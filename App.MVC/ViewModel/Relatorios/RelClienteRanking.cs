using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.MVC.ViewModels.Relatorios
{
    public class RelClienteRanking
    {
        public string NomeCliente { get; set; }

        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}