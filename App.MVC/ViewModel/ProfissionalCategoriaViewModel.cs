using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.MVC.ViewModels
{
    public class ProfissionalCategoriaViewModel
    {
        [Key]
        public int Id { get; set; }

        public int ProfissionalId { get; set; }

        public int CategoriaId { get; set; }

        [NotMapped]
        public string NomeCategoria { get; set; }
    }
}