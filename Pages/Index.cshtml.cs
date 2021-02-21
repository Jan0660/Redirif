using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedirectPage.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(Name = "Title")] public string? Title { get; set; }

        [BindProperty(Name = "Url")] public string? Url { get; set; }

        //[BindProperty(Name = "ImageUrl")]
        //public string? ImageUrl { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnPost(string ImageUrl, string Description, bool SmallImage, string EmbedColor, string SiteName)
        {
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