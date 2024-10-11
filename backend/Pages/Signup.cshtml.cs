using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace library.Pages
{
    public class SignupModel : PageModel
    {

        [BindProperty]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Login { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный электронный адрес")]
        public string Email { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Не заполнено поле пароля")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля - 6 символов")]
        public string Password { get; set; } = "";
    }
}