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
