using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShortLivedConsoleApp
{
    public interface IWorker
    {
        void Run(string[] args);
        void Command1();
        void Command2();
    }

    public class Worker : IWorker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public void Run(string[] args)
        {
            // your code here
            _logger.LogInformation("Worker run");
        }

        public void Command1()
        {
            _logger.LogInformation("Command 1");
            return;
        }

        public void Command2()
        {
            _logger.LogInformation("Command 2");
            return;
        }
    }
}
