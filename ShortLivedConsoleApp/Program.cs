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
        public static async Task Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                await host.StartAsync();
                var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
                var config = host.Services.GetRequiredService<IConfiguration>();

                // do work here / get your work service ...
                string configValue = config.GetValue<string>("Test");
                Console.WriteLine(configValue);

                lifetime.StopApplication();
                await host.WaitForShutdownAsync();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
               .UseConsoleLifetime()
               .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Warning))
               .ConfigureServices((hostContext, services) =>
               {
                   //services.Configure<MyServiceOptions>(hostContext.Configuration);
                   //services.AddHostedService<MyService>();
                   //services.AddSingleton(Console.Out);
               });

            return hostBuilder;
        }
    }
}