using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Redirif.Pages
{
    public class ApiOnly : PageModel
    {
        public ActionResult OnGet()
        {
            if (!Program.Config.ApiOnly)
                return Redirect("/");
            return Page();
        }
    }
}