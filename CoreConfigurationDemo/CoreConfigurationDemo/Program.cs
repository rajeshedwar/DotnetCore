using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreConfigurationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>{
                config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("myconfig.json")
                .AddXmlFile("mydata.xml")
                //.AddIniFile("mydata.ini")
                //.AddInMemoryCollection(dicitonaryObj)
                //.AddKeyPerFile("fileName")
                .AddEnvironmentVariables("ASPNETCORE_")
                .AddCommandLine(args)
                .Build();
            })
             .UseStartup<Startup>();
    }
}
