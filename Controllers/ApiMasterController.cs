using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace RedirectPage.Controllers
{
    [Route("/api/master")]
    public class ApiMasterController : Controller
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        public ApiMasterController()
        {
            Console.WriteLine("h");
        }
        [HttpGet("list")]
        public object ListTokens()
        {
            return ApiTokens.Tokens;
        }

        [HttpPost("create")]
        public string CreateNewToken()
            => ApiTokens.Create();

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteToken()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var index = ApiTokens.Tokens.IndexOf(await reader.ReadToEndAsync());
                if (index == -1)
                    return NotFound();
                ApiTokens.Tokens.RemoveAt(index);
            }

            return Ok();
        }

        [HttpGet("listRedirects")]
        public string ListRedirects()
        {
            if (Request.Query.ContainsKey("start"))
            {
                // returns items starting from <start>
                if(!Request.Query.ContainsKey("size"))
                    return JsonConvert.SerializeObject(Redirects.Dict.Skip(int.Parse(Request.Query["start"])), _jsonSerializerSettings);
                // returns <size> items starting from <start>
                else
                    return JsonConvert.SerializeObject(Redirects.Dict.Skip(int.Parse(Request.Query["start"])).Take(int.Parse(Request.Query["size"])), _jsonSerializerSettings);
            }
            // return first <size> items
            if(Request.Query.ContainsKey("size"))
                return JsonConvert.SerializeObject(Redirects.Dict.Take(int.Parse(Request.Query["size"])), _jsonSerializerSettings);
            return JsonConvert.SerializeObject(Redirects.Dict, _jsonSerializerSettings);
        }

        [HttpDelete("deleteRedirect")]
        public async Task<IActionResult> DeleteRedirect()
        {
            using StreamReader reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var redirect = Redirects.Dict.FirstOrDefault(r => r.Key == body);
            if (redirect.Key == null)
                return NotFound();
            Redirects.Dict.Remove(redirect.Key);
            return Ok();
        }

        // this is retarded, makes asp.net return HTTP 400/Bad Request instead of fucking 401/Unauthorized
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (Program.ApiMasterToken == null)
                throw new Exception("API disabled.");
            if (Request.Headers["Api-Token"] != Program.ApiMasterToken)
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}