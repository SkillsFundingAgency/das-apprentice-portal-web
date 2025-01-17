using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticePortal.Web.Startup;

namespace SFA.DAS.ApprenticePortal.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAzureTableConfiguration()
                .UseStartup<ApplicationStartup>()
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetry();
                });
    }
}
