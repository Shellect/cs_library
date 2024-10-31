using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        public UserController(ApplicationContext context) {
            _context = context;
        }

        public async Task<JsonResult> Index()
        {
            try
            {
                List<User> users = await _context.Users.ToListAsync();
                return Json(users);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}