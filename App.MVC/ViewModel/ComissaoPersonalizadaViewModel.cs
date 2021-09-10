using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ControleDeSalao.Domain.Entities;

namespace App.MVC.ViewModels
{
    public class ComissaoPersonalizadaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public int ServicoId { get; set; }

        public int ProfissionalId { get; set; }

        public double Comissao { get; set; }

        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        public virtual ProfissionalViewModel Profissional { get; set; }

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