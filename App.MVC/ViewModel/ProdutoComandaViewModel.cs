using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.MVC.ViewModels
{
    public class ProdutoComandaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int ComandaId { get; set; }

        public int ProdutoId { get; set; }

        public double Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public double ValorProduto { get; set; }

        [DataType(DataType.Currency)]
        public double ValorTotal { get; set; }

        private string _nomeProduto;

        [NotMapped]
        [Required]
        public string NomeProduto
        {
            get
            {
                if (string.IsNullOrEmpty(_nomeProduto))
                    if (ProdutoId != 0)
                    {
                        var prd = Produto;
                        _nomeProduto = prd == null ? string.Empty : prd.Descricao;
                    }
                    else
                        _nomeProduto = string.Empty;

                return _nomeProduto;
            }
            set { _nomeProduto = value; }
        }
        
        public virtual ProdutoViewModel Produto  { get; set; }

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