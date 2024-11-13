using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace app.Controllers
{
    public class AccountController(ApplicationContext _context) : Controller
    {
        private readonly ApplicationContext context = _context;

        [HttpPost]
        public async Task<IResult> Registration([FromBody] RegistrationViewModel model)
        {
            // Валидация отпраленных пользователем данных
            if (!ModelState.IsValid)
            {
                // Выбираем список ошибок
                IEnumerable<string> Errors = ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage);
                return Results.Json(new RegistrationResponseViewModel { Errors = Errors, Success = false });
            }

            // Выбираем пользователя из базы данных
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email ==  model.Email || u.Login == model.Login);
            // Если найден - сообщаем в тексте ошибки
            if (user != null) 
            {
                return Results.Json(new RegistrationResponseViewModel { Success = false, Errors = [
                    "Пользователь с такими учетными данными уже существует"
                ] });
            }
            // Создаем нового пользователя в БД
            // TODO: подумать про automapper
            context.Users.Add(new User { Login = model.Login, Email = model.Email, Password = model.Password });
                    await context.SaveChangesAsync();
            // Аутентификация пользователя
            await Authenticate(model.Email);
            // Возвращаем результат запроса
            return Results.Json(new RegistrationResponseViewModel { Success = true });
        }

        public async Task<IResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Выбираем список ошибок
                IEnumerable<string> Errors = ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage);
                return Results.Json(new RegistrationResponseViewModel { Errors = Errors, Success = false });
            }
             // Выбираем пользователя из базы данных
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email ==  model.Email && u.Password == model.Password);
            if (user == null)
            {
                return Results.Json(new RegistrationResponseViewModel { Success = false, Errors = [
                    "Неверный логин или пароль"
                ] });
            }
            // Аутентификация пользователя
            await Authenticate(model.Email);
            // Возвращаем результат запроса
            return Results.Json(new RegistrationResponseViewModel { Success = true });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        public IActionResult Test()
        {
            return Content($"Name: {User.Identity.Name} Role: {User.Identity.IsAuthenticated}");
        }
 
        private async Task Authenticate(string Email)
        {
            List<Claim> claims = [
                new(ClaimTypes.Name, Email),
                new(ClaimTypes.Role, "User")
            ];
            ClaimsIdentity identity = new(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal principal = new(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}