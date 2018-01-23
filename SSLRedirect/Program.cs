using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SSLRedirect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    var config = options.ApplicationServices.GetRequiredService<IConfiguration>();
                    var httpPort = int.Parse(config["httpPort"]);
                    var httpsPort = int.Parse(config["httpsPort"]);
                    var certificatePath = config["certificatePath"];
                    var certificatePwd = config["certificatePwd"];
                    var cert = new X509Certificate2(certificatePath, certificatePwd);

                    var addresses = new List<IPAddress>
                    {
                        IPAddress.IPv6Loopback,
                        IPAddress.Loopback
                    };

                    foreach (var address in addresses)
                    {
                        options.Listen(address, httpPort);
                        options.Listen(address, httpsPort, listenOptions => listenOptions.UseHttps(cert));
                    }
                })
                .Build();
    }
}
