using System.ComponentModel.DataAnnotations;

namespace app.ViewModels
{
    public class AuthenticateRequest
    {
        [Required]
        public required string AccessToken { get; set; }
    }
}