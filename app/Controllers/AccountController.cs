using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.ViewModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.Login == model.Login);
            // Если найден - сообщаем в тексте ошибки
            if (user != null)
            {
                return Results.Json(new RegistrationResponseViewModel
                {
                    Success = false,
                    Errors = [
                    "Пользователь с такими учетными данными уже существует"
                ]
                });
            }
            // Создаем нового пользователя в БД
            context.Users.Add(new User { Login = model.Login, Email = model.Email, Password = model.Password });
            await context.SaveChangesAsync();
            // Аутентификация пользователя
            var encodedJwt = Authenticate(model.Email);
            return  Results.Json(new {token = encodedJwt});
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
            var encodedJwt = Authenticate(model.Email);
            return  Results.Json(new {token = encodedJwt});
        }

        public IActionResult Logout()
        {
            return Ok();
        }

        private string Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName)
            };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}