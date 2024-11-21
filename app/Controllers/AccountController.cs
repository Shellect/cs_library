using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.ViewModels;
using app.Services;
using System.Security.Claims;

namespace app.Controllers
{
    public class AccountController(ApplicationContext context, ITokenService tokenService, IUserService userService) : Controller
    {
        [HttpPost]
        public async Task<AuthenticateResponse> Registration([FromBody] RegistrationRequest model)
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
                    Success = false,
                    Errors = Errors
                };
            }

            // Выбираем пользователя из базы данных
            User? existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.Login == model.Login);
            // Если найден - сообщаем в тексте ошибки
            if (existingUser != null)
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
            User user = await userService.Create(model);
            return Authenticate(user);
        }

        [HttpPost]
        public async Task<AuthenticateResponse> Login([FromBody] LoginRequest model)
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

        [HttpPost]
        public AuthenticateResponse RefreshToken(AuthenticateRequest model)
        {
            // Find user
            User? user = userService.GetUser(Request.Cookies["refreshToken"]);
            if(!ModelState.IsValid || user is null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new AuthenticateResponse
                {
                    Success = false,
                    Errors = [
                        "Invalid client request"
                    ]
                };
            }

            // Revoke refresh token
            CreateRefreshToken(user);
            
            // Refresh access token
            ClaimsPrincipal principal = tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            IEnumerable<Claim> claims = principal.Claims;

            return new AuthenticateResponse
            {
                Success = true,
                Login = user.Login,
                AccessToken = tokenService.GetAccessToken(claims)
            };
        }

        [HttpPost]
        public AuthenticateResponse RevokeToken()
        {
            User? user = userService.GetUser(Request.Cookies["refreshToken"]);
            if (user is null)
            {
                return new AuthenticateResponse
                {
                    Success = false,
                    Errors = [
                        "Invalid client request"
                    ]
                };
            }
            return Authenticate(user);
        }

        /// <summary>
        /// Append cookie with refresh token to the http response
        /// </summary>
        private AuthenticateResponse Authenticate(User user)
        {
            CreateRefreshToken(user);
            // создаем один claim
            List<Claim> claims =
            [
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.Email, user.Email)
            ];
            return new AuthenticateResponse
            {
                Success = true,
                Login = user.Login,
                AccessToken = tokenService.GetAccessToken(claims)
            };
        }

        private void CreateRefreshToken(User user)
        {
            string RefreshToken = tokenService.GetRefreshToken();
            user.RefreshToken = RefreshToken;
            context.SaveChanges();

            CookieOptions cookieOptions = new()
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("refreshToken", user.RefreshToken, cookieOptions);
        }
    }
}