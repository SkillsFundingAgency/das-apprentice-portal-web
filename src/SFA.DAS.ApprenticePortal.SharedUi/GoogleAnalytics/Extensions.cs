using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics
{
    public static class Extensions
    {
        public static bool GoogleAnalyticsIsEnabled(this ViewDataDictionary viewData)
            => !string.IsNullOrWhiteSpace(GetConfiguration(viewData)?.GoogleTagManagerId);

        public static string? GetGoogleTagManagerId(this ViewDataDictionary viewData)
            => GetConfiguration(viewData)?.GoogleTagManagerId;

        private static GoogleAnalyticsConfiguration? GetConfiguration(ViewDataDictionary viewData)
            => viewData.TryGetValue(ViewDataKeys.GoogleAnalyticsConfiguration, out var section)
                ? section as GoogleAnalyticsConfiguration
                : null;

        public static IServiceCollection EnableGoogleAnalytics(this IServiceCollection services, GoogleAnalyticsConfiguration configuration)
        {
            services.Configure<MvcOptions>(options =>
                options.Filters.Add(new EnableGoogleAnalyticsAttribute(configuration)));
            return services;
        }
    }
}