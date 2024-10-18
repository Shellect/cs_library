using Microsoft.AspNetCore.Mvc;
using app.Models;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}