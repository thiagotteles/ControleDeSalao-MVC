using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class ServicoPreCadastradoViewModel
    {
        [Key]
        public int Id { get; set; }

        public int CategoriaId { get; set; }

        public string Descricao { get; set; }

        public bool Status { get; set; }
    }
}