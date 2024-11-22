using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.ViewModels;
using System.Security.Claims;
using app.Services;

namespace app.Controllers
{
    public class AccountController(ApplicationContext context, ITokenService tokenService, IUserService userService) : Controller
    {
        [HttpPost]
        public async Task<AuthenticateResponse> Registration([FromBody] RegistrationViewModel model)
        {
            // Валидация отпраленных пользователем данных
            if (!ModelState.IsValid)
            {
                // Выбираем список ошибок
                IEnumerable<string> Errors = ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage);
                return new AuthenticateResponse { Errors = Errors, Success = false };
            }

            // Выбираем пользователя из базы данных
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.Login == model.Login);
            // Если найден - сообщаем в тексте ошибки
            if (user != null)
            {
                return new AuthenticateResponse
                {
                    Success = false,
                    Errors = [
                    "Пользователь с такими учетными данными уже существует"
                ]
                };
            }
            // Создаем нового пользователя в БД
            User newUser = await userService.Create(model);
            // Аутентификация пользователя
            return Authenticate(newUser);
        }

        [HttpPost]
        public async Task<AuthenticateResponse> Login([FromBody] LoginViewModel model)
        {
            // Валидация отпраленных пользователем данных
            if (!ModelState.IsValid)
            {
                // Выбираем список ошибок
                IEnumerable<string> Errors = ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage);
                return new AuthenticateResponse
                {
                    Errors = Errors,
                    Success = false
                };
            }
            // Выбираем пользователя из базы данных
            User? user = await userService.GetUser(model);
            if (user == null)
            {
                return new AuthenticateResponse
                {
                    Success = false,
                    Errors = [
                        "Неверный логин или пароль"
                    ]
                };
            }
            return Authenticate(user);
        }

        public IActionResult Logout()
        {
            return Ok();
        }

        /// <summary>
        /// Append cookie with refresh token to the http response
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private AuthenticateResponse Authenticate(User user)
        {
            // создаем набор claim
            List<Claim> claims =
            [
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.Email, user.Email)
            ];
            // Возвращаем access token
            return new AuthenticateResponse
            {
                Success = true,
                Login = user.Login,
                AccessToken = tokenService.GetAccessToken(claims)
            };
        }
    }
}