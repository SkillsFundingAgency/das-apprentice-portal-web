using System;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HashingService;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public static class HashingStartup
    {
        public static IServiceCollection AddHashingService(
            this IServiceCollection services,
            HashingConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton<IHashingService>(
                new HashingService.HashingService(
                    configuration.AllowedHashstringCharacters,
                    configuration.Hashstring));

            return services;
        }
    }

    public class HashingConfiguration
    {
        public string AllowedHashstringCharacters { get; set; } = null!;
        public string Hashstring { get; set; } = null!;
    }
}

