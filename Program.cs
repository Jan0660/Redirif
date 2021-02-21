using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedirectPage
{
    public static class Program
    {
        public static string ApiMasterToken = null;
        public static void Main(string[] args)
        {
            if(!Directory.Exists("./data"))
                Directory.CreateDirectory("./data");
            if (File.Exists("./redirects.json"))
            {
                File.Move("./redirects.json", "./data/redirects.json");
            }
            if(File.Exists("./data/redirects.json"))
                Redirects.Dict = JsonConvert.DeserializeObject<Dictionary<string, RedirectInfo>>(File.ReadAllText("./data/redirects.json"));
            if(!File.Exists("./data/tokens.json"))
                File.WriteAllText("./data/tokens.json", "[]");
            ApiTokens.Tokens = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("./data/tokens.json"));
                var masterTokenVar = Environment.GetEnvironmentVariable("API_MASTER_TOKEN");
            if(masterTokenVar == null)
                Console.WriteLine("`API_MASTER_TOKEN` is not set in your environment variables, redirect/* and master/* API endpoints disabled.");
            else
            {
                ApiMasterToken = masterTokenVar;
                Console.WriteLine($"API enabled with {ApiTokens.Tokens.Count} regular api token(s).");
                if(ApiMasterToken.Length < 16)
                    Console.WriteLine("Short master API token, recommended is a long string of random characters.");
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static string SanitizeHtml(this string str)
            => str == null ? "" : str.Replace("&", "&amp;")
                .Replace("<", "&lt")
                .Replace(">", "&gt")
                .Replace("\"", "&quot;")
                //.Replace("'", "#039;")
            ;
    }
}
