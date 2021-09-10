using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Status { get; set; }
    }
}