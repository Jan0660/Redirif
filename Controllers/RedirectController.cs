using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RedirectPage.Controllers
{
    [Route("/r/")]
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
        public ContentResult Index(string name)
        {
            var redir = Redirects.Dict[name];
            if (Request.Headers["User-Agent"].ToString().Contains("https://discordapp.com"))
            {
                Console.WriteLine("based discord");
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = boil($@"<meta property=""og:title"" content=""{redir.Title}"" />
<meta property=""og:description"" content=""{redir.Description}"" />
<meta name=""twitter:card"" content=""bruh"" />
<meta name=""twitter:image"" content=""{redir.ImageUrl}"" />")
                };
            }
            Response.Redirect(redir.Url);
            Console.WriteLine("based human");
            return Content("poggers");
        }
        [Route("/elpoggers/champ")]
        public string boil(string str)
        {
            return BoilerPlateStart + str + BoilerPlateEnd;
        }
        [Route("/poggers/champ")]
        public ContentResult bruh(string str)
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = boil("<meta property=\"og:title\" content=\"bruh\" />")
            };
        }
    }
}
