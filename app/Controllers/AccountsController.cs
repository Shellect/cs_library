using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace app.Controllers
{
    public class AccountsController(ApplicationContext context) : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IResult> Registration(RegistrationModel model)
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
                User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    return Results.Json(new { success = false, message = "Wrong login or password" });
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
    }
}