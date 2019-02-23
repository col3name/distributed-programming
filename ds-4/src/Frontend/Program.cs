using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {
            Console.WriteLine("Hello world!");
            JObject jObject = JObject.Parse(File.ReadAllText(@".\config\config.json"));
            string frontendUrl = jObject.GetValue("frontendUrl").ToString();
            string frontendPort = jObject.GetValue("frontendPort").ToString();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(frontendUrl + ":" + frontendPort)
                .Build();
        }
    }
}
