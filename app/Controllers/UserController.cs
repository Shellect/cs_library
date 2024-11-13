using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    public class UserController(ApplicationContext db) : Controller
    {
        [Authorize]
        public async Task<IResult> Index()
        {
            List<User> users = await db.Users.ToListAsync();
            return Results.Json(users);
        }
    }
}