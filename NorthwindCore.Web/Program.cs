using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace NorthwindCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // clears all pre-configured configurations
            // builder.Sources.Clear();

            //builder
            //    .AddJsonFile("appsettings.json", false, true)
            //    // .AddEnvironmentVariables("NorthwindCore_");
            //    .AddEnvironmentVariables();
        }
    }
}
