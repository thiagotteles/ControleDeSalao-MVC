using System.ComponentModel.DataAnnotations;

namespace App.MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nome do Salão")]
        public string Nome { get; set; }

        public string Login { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = ("Digite a Senha"))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita a Senha")]
        [Compare("Password", ErrorMessage = "As Senhas digitadas não são iguais.")]
        public string ConfirmPassword { get; set; }

        public string CodigoPromocional { get; set; }
    }
}