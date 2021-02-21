using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace RedirectPage.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/api/redirect")]
    public class ApiRedirectController : Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpPost("create")]
        public async Task<IActionResult> CreateRedirect()
        {
            string json;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();
            }
            var redirectInfo = JsonConvert.DeserializeObject<RedirectInfo>(json);
            // todo: add checks for too long SiteName, Description etc
            if (redirectInfo.Url == null | redirectInfo.Url == "")
                return BadRequest("Url cannot be null");
            return Content(Redirects.Create(redirectInfo));
        }

        // this is retarded, makes asp.net return HTTP 400/Bad Request instead of fucking 401/Unauthorized
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (Program.ApiMasterToken == null)
                throw new Exception("API disabled.");
            if (!(Request.Headers["Api-Token"] == Program.ApiMasterToken
                | ApiTokens.Tokens.Contains(Request.Headers["Api-Token"])))
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}