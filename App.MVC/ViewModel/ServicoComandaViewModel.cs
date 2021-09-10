using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class ServicoComandaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int ComandaId { get; set; }

        public int? ServicoId { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        private string _nomeServico;

        [NotMapped]
        [Required]
        public string NomeServico
        {
            get
            {
                if (string.IsNullOrEmpty(_nomeServico))
                    if (ServicoId != 0)
                    {
                        var ser = Servico;
                        _nomeServico = ser != null ? ser.Descricao : string.Empty;
                    }
                    else
                        _nomeServico = string.Empty;

                return _nomeServico;
            }
            set { _nomeServico = value; }
        }

        public virtual ServicoViewModel Servico { get; set; }
 
        private int _index;

        [NotMapped]
        public int Index
        {
            get
            {
                if (_index == 0)
                    _index = new Random().Next();

                return _index;
            }
            set { _index = value; }
        }
    }
}