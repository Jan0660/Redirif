using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Redirif.Controllers
{
    [Route("/api/redirect")]
    [EnableCors("BruhPolicy")]
    public class ApiRedirectController : Controller
    {
        [HttpPost("create")]
        [EnableCors("BruhPolicy")]
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
                return BadRequest("Url must be specified");
            return Content(Redirects.Create(redirectInfo));
        }

        // this is retarded, makes asp.net return HTTP 400/Bad Request instead of fucking 401/Unauthorized
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (Program.Config.ApiMasterToken == null)
                throw new Exception("API disabled.");
            if (!(Request.Headers["Api-Token"] == Program.Config.ApiMasterToken
                | ApiTokens.Tokens.Contains(Request.Headers["Api-Token"])))
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}