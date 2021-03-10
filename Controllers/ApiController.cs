using System;
using System.Reflection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Redirif.Controllers
{
    [Route("/api")]
    [EnableCors("BruhPolicy")]
    public class ApiController : Controller
    {
        [HttpGet]
        [EnableCors("BruhPolicy")]
        public object Index()
        {
            Response.ContentType = "application/json";
            return new
            {
                AppVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString(),
                FrameworkVersion = Environment.Version.ToString(),
                TotalRedirects = Redirects.Dict.Count,
                APIEnabled = Program.Config.ApiMasterToken != null
            };
        }
    }
}