using System.ComponentModel.DataAnnotations;

namespace app.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is reqired")]
        public string Password { get; set; } = "";
    }
}