using Microsoft.AspNetCore.Mvc.RazorPages;

namespace library.Pages
{
    public class IndexModel : PageModel
    {
        public string Title { get; }
        public IndexModel()
        {
            Title = DateTime.Now.ToShortDateString();
        }
        public void OnGet()
        {
        }
    }
}