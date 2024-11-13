using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Antiforgery;


namespace app.Controllers
{
    public class AccountController(ApplicationContext context) : Controller
    {
        [HttpPost]
        public async Task<IResult> Registration([FromBody]RegistrationModel model)
        {
            try
            {
                if ( !ModelState.IsValid ) 
                {
                    IEnumerable<string> Errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);
                    return Results.Json(new RegistrationResponseModel { Errors = Errors, Success = false });
                }
                // Проверяем существование пользователя
                User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.Login == model.Login);
                // Если найден - сообщаем в тексте ошибки
                if (user != null)
                {
                    return Results.Json(new RegistrationResponseModel
                    {
                        Success = false,
                        Errors = [
                            "Пользователь с такими учетными данными уже существует"
                        ]
                    });
                }

                context.Users.Add(new User { Login = model.Login, Email = model.Email, Password = model.Password });
                await context.SaveChangesAsync();
                return Results.Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Results.Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IResult> Login([FromBody]LoginModel model)
        {
            try
            {
                if ( !ModelState.IsValid ) 
                {
                    IEnumerable<string> Errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);
                    return Results.Json(new RegistrationResponseModel { Errors = Errors, Success = false });
                }
                // Проверяем существование пользователя
                User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user == null)
                {
                    return Results.Json(new RegistrationResponseModel
                    {
                        Success = false,
                        Errors = [
                            "Проверьте учетные данные пользователя"
                        ]
                    });
                }
                return Results.Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Results.Json(new { success = false, message = ex.Message });
            }
        }

        private async Task Authenticate(string Email)
        {
            List<Claim> claims = [
                new(ClaimTypes.Name, Email)
            ];
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal principal = new(id);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}