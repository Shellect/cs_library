using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationContext context;
        public List<User> Users { get; private set; } = new();
        public string Title { get; }
        public IndexModel(ApplicationContext context)
        {
            this.context = context;
            Title = DateTime.Now.ToShortDateString();
        }
        public void OnGet()
        {
            Users = context.Users.ToList();
        }

    }
}