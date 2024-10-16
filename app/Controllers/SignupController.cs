using Microsoft.AspNetCore.Mvc;
using app.Models;

namespace app.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Signup page";
            return View();
        }
    }
}