using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext db;
        public UserController(ApplicationContext context) {
            db = context;
        }

        public async Task<JsonResult> Index()
        {
            List<User> users = await db.Users.ToListAsync();
            return Json(users);
        }
    }
}