using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Redirif
{
    public static class Redirects
    {
        public static Dictionary<string, RedirectInfo> Dict = new Dictionary<string, RedirectInfo>();

        public static void Add(string name, RedirectInfo info)
        {
            Dict[name] = info;
            File.WriteAllText("./data/redirects.json", JsonConvert.SerializeObject(Dict, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }

        public static bool Exists(string name)
        {
            return name == null ? false : Dict.ContainsKey(name);
        }

        public static string Create(RedirectInfo redirectInfo)
        {
            string name = null;
            var rng = new Random();
            while (name == null || Redirects.Exists(name))
            {
                byte[] bytes = new byte[3];
                rng.NextBytes(bytes);
                name = Convert.ToBase64String(bytes).Replace("/", "-").Replace("+", "_");
            }

            Redirects.Add(name, redirectInfo);
            return name;
        }
    }
}