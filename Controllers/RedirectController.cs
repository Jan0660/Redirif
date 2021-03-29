using System;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Redirif.Controllers
{
    [Route("/r/")]
    [EnableCors("BruhPolicy")]
    public class RedirectController : Controller
    {
        const string BoilerPlateStart = @"<!DOCTYPE html>
<html>
<head>
<title>poggers</title>";
        const string BoilerPlateEnd = @"
</head>
<body>
</body>
</html>";
        [Route("/r/{name}")]
        public IActionResult Index(string name)
        {
            Console.WriteLine(Request.Headers["User-Agent"].ToString());
            RedirectInfo redir;
            try
            {
                redir = Redirects.Dict[name];
            }
            catch
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = IsNotHuman() ? 200 : 404,
                    Content = BoilerPlateStart + GetRedirectMetadataHtml(new RedirectInfo()
                    {
                        Title = "Redirect link not found",
                        EmbedColor = "ff0000",
                        SiteName = "Redirif"
                    })
                    + @"</head><body>Redirect link not found</body></html>"
                };
            }

            if (IsNotHuman())
            {
                Console.WriteLine("based discord");
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = boil(GetRedirectMetadataHtml(redir))
                };
            }
            Response.Redirect(redir.Url);
            Console.WriteLine("based human");
            return Content("poggers");
        }
        private string boil(string str)
        {
            return BoilerPlateStart + str + BoilerPlateEnd;
        }

        private string GetRedirectMetadataHtml(RedirectInfo redir)
        {
            return $@"<meta property=""og:title"" content=""{redir.Title.SanitizeHtml()}"" />
<meta property=""og:description"" content=""{redir.Description.SanitizeHtml()}"" />
<meta property=""og:site_name"" content=""{redir.SiteName.SanitizeHtml()}"" />
<meta name=""twitter:image"" content=""{redir.ImageUrl.SanitizeHtml()}"" />
<meta name=""theme-color"" content=""#{redir.EmbedColor.SanitizeHtml()}"" />
<meta data-react-helmet=""true"" name=""msapplication-TileColor"" content=""#{redir.EmbedColor.SanitizeHtml()}"">
<meta data-react-helmet=""true"" name=""theme-color"" content=""#{redir.EmbedColor.SanitizeHtml()}"">" +
                        (redir.SmallImage ? "" : @$"<meta name=""twitter:card"" content=""summary_large_image"">");
        }

        private bool IsNotHuman()
            => Request.Headers["User-Agent"].ToString().Contains("https://discordapp.com")
             | Request.Headers["User-Agent"].ToString().Contains("Twitterbot");
    }
}
