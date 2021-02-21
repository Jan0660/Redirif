using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace RedirectPage
{
    public static class ApiTokens
    {
        private static RNGCryptoServiceProvider rng = new ();
        public static List<string> Tokens;
        
        public static string Create()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string res = "";
            for(int i = 0; i < 256;)
            {
                var n = new byte[1];
                rng.GetBytes(n);
                if (n[0] < chars.Length)
                {
                    res += chars[n[0]];
                    i++;
                }
            }
            Tokens.Add(res);
            File.WriteAllText("./data/tokens.json", JsonConvert.SerializeObject(Tokens));
            return res;
        }
    }
}