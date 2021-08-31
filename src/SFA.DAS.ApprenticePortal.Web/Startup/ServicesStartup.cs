using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.Web.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using SFA.DAS.Http.Configuration;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public static class ServicesStartup
    {
        public static IServiceCollection AddOuterApi(
            this IServiceCollection services,
            OuterApiConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddTransient<Http.MessageHandlers.DefaultHeadersHandler>();
            services.AddTransient<Http.MessageHandlers.LoggingMessageHandler>();
            services.AddTransient<Http.MessageHandlers.ApimHeadersHandler>();

            global::NLog.LogManager.GetLogger("ServicesStartup").Info("ApiBaseUrl: {url}", configuration.ApiBaseUrl);

            services
                .AddRestEaseClient<IOuterApiClient>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<Http.MessageHandlers.DefaultHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.ApimHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.LoggingMessageHandler>();

            services.AddTransient<IApimClientConfiguration>((_) => configuration);

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IMenuVisibility, MenuVisibility>()
                .AddScoped<ApprenticeApi>();
            return services;
        }
    }

    public class OuterApiConfiguration : IApimClientConfiguration
    {
        public string ApiBaseUrl { get; set; } = null!;
        public string SubscriptionKey { get; set; } = null!;
        public string ApiVersion { get; set; } = null!;
    }
}