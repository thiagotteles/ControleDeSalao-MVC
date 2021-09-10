using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleDeSalao.Domain.Entities;

namespace App.MVC.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Direitos { get; set; }

        [Display(Name = "Data do Cadastro")]
        public DateTime DtCadastro { get; set; }

        [Display(Name = "Data Ult. Login")]
        public DateTime? DtUltLogin { get; set; }

        [Display(Name = "IP")]
        public string IpUltLogin { get; set; }

        [Display(Name = "É um profissional? informe o profissional")]
        public int? ProfissionalId { get; set; }

        public string Tutorial { get; set; }

        public string Cargo { get; set; }

        public string UrlAvatar { get; set; }

        [NotMapped]
        public Funcionalidade Acesso { get; set; }

        [NotMapped]
        public EmpresaViewModel Empresa { get; set; }

    }
}