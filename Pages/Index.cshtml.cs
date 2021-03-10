using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Redirif.Pages
{
    public class IndexModel : PageModel
    {
#pragma warning disable 8632
        [BindProperty(Name = "Title")] public string? Title { get; set; }

        [BindProperty(Name = "Url")] public new string? Url { get; set; }
#pragma warning restore 8632
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public ActionResult OnGet()
        {
            if (Program.Config.ApiOnly)
                return Redirect("/ApiOnly");
            return Page();
        }

        public void OnPost(string ImageUrl, string Description, bool SmallImage, string EmbedColor, string SiteName)
        {
            if (Program.Config.ApiOnly)
            {
                Response.Redirect("/ApiOnly");
                return;
            }
            if (Url == null)
            {
                Response.Redirect("/error");
                return;
            }

            var name = Redirects.Create(new RedirectInfo
            {
                Url = Url,
                Title = Title,
                ImageUrl = ImageUrl,
                Description = Description,
                SmallImage = SmallImage,
                EmbedColor = EmbedColor ?? "2F3136",
                SiteName = SiteName
            });
            Response.Redirect($"/Created?name={name}");
        }
    }
}