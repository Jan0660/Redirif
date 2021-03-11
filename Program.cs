using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Redirif
{
    public static class Program
    {
        //public static string ApiMasterToken = null;
        public static Configuration Config;
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
            if(!File.Exists("./data/config.json"))
                File.WriteAllText("./data/config.json", JsonConvert.SerializeObject(new Configuration(), Formatting.Indented));
            Config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("./data/config.json"));
            ApiTokens.Tokens = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("./data/tokens.json"));
            var masterTokenVar = Environment.GetEnvironmentVariable("API_MASTER_TOKEN");
            if(masterTokenVar != null)
                Config.ApiMasterToken = masterTokenVar;
            if (Config.ApiMasterToken == null)
            {
                Console.WriteLine("The API master token is not set in your configuration(config.json or environment variable), redirect/* and master/* API endpoints disabled.");
            }
            else
            {
                Console.WriteLine($"API enabled with {ApiTokens.Tokens.Count} nerd API token(s).");
                if(Config.ApiMasterToken.Length < 16)
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
