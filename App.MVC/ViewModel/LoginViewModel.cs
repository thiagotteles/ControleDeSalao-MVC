using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = ("Digite a Senha"))]
        public string Password { get; set; }
    }
}