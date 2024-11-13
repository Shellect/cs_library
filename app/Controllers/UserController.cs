using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class UserController(ApplicationContext db) : Controller
    {
        public async Task<IResult> Index()
        {
            List<User> users = await db.Users.ToListAsync();
            return Results.Json(users);
        }
    }
}