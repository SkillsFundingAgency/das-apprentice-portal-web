using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;
using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Startup
{
    public static class SharedUiStartup
    {
        public static IServiceCollection AddSharedUi(
            this IServiceCollection services, ISharedUiConfiguration configuration, Action<SharedUiOptions> options)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton(configuration.ApplicationUrls);
            services.AddTransient(s => s.GetRequiredService<Controller>());
            services.AddTransient<NavigationUrlHelper>();
            services.AddTransient<NavigationHelper>();
            services.AddScoped<CachedApprenticeshipService>();

            options?.Invoke(new SharedUiOptions(services, configuration));

            return services;
        }
    }

    public class SharedUiOptions
    {
        private readonly IServiceCollection services;
        private readonly ISharedUiConfiguration configuration;

        public SharedUiOptions(IServiceCollection services, ISharedUiConfiguration configuration)
        {
            this.services = services;
            this.configuration = configuration;
        }

        public void EnableGoogleAnalytics()
            => services.EnableGoogleAnalytics(configuration.GoogleAnalytics);

        public void EnableZendesk()
            => services.SetZenDeskConfiguration(configuration.Zendesk);

        public void SetCurrentNavigationSection(NavigationSection confirmMyApprenticeship)
            => services.SetCurrentNavigationSection(confirmMyApprenticeship);
    }
}