using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using SFA.DAS.ApprenticeCommitments.Web.Startup;
using SFA.DAS.ApprenticePortal.Web.Startup;

namespace SFA.DAS.ApprenticePortal.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            NLogStartup.ConfigureNLog();
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAzureTableConfiguration()
                .UseStartup<ApplicationStartup>()
                .UseNLog();
    }
}