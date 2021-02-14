using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RedirectPage
{
    public static class Redirects
    {
        public static Dictionary<string, RedirectInfo> Dict = new Dictionary<string, RedirectInfo>();

        public static void Add(string name, RedirectInfo info)
        {
            Dict[name] = info;
            File.WriteAllText("./redirects.json", JsonConvert.SerializeObject(Dict, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }

        public static bool Exists(string name)
        {
            return name == null ? false : Dict.ContainsKey(name);
        }
    }
}