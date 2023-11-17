using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ShortLivedConsoleApp
{
    // adapted from: https://stackoverflow.com/questions/66996319/the-correct-way-to-create-a-net-core-console-app-without-background-services
    public class Program
    {
        private static readonly Dictionary<string, string> s_switchMappings = new Dictionary<string, string>()
        {
            { "--c", "command" },
        };

        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Getting worker...");

            var worker = host.Services.GetRequiredService<IWorker>();

            worker.Run(args);

            logger.LogInformation("Done.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory)
                          .AddCommandLine(args, s_switchMappings);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // register all your services here you'd like to use dependency injection with
                    services.AddTransient<IWorker, Worker>();
                });
        }
    }
}