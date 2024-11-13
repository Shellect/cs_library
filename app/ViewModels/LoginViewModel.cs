using System.ComponentModel.DataAnnotations;

namespace app.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is reqired")]
        [MinLength(5, ErrorMessage ="Password length must be more than 5 symbols")]
        public string? Password { get; set; }
    }
}