using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kiona.io.Configuration
{
    public class Configuration
    {
        public static string Token { get; set; }
        public static char Prefix { get; set; }

        public Configuration()
        {
            var data = JsonConvert.DeserializeObject<API>(File.ReadAllText(@"C:\Users\timot\Documents\C#\kiona.io\kiona.io\Configuration\Data.json"));

            Token = data.Bot.token;
            Prefix = Convert.ToChar(data.Bot.prefix);
        }
    }
}
