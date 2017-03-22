using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
            

            var pfxFile = "ssl/dev.pfx";
            X509Certificate2 certificate = new X509Certificate2(pfxFile, "password");


            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel( cfg => cfg.UseHttps(certificate))
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("https://*:4430")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
