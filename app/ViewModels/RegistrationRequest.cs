using System.ComponentModel.DataAnnotations;

namespace app.ViewModels
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is reqired")]
        [MinLength(5, ErrorMessage ="Password length must be more than 5 symbols")]
        public string Password { get; set; } = "";
        
        [Compare("Password", ErrorMessage = "Passwords must be identical")]
        public string ConfirmPassword { get; set; } = "";
    }
}