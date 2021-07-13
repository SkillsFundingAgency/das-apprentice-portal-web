﻿using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
//using SFA.DAS.ApprenticeCommitments.Web.Services;
//using SFA.DAS.ApprenticeCommitments.Web.Services.OuterApi;
//using SFA.DAS.ApprenticeCommitments.Web.TagHelpers;
using SFA.DAS.Http.Configuration;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public static class ServicesStartup
    {
        //public static IServiceCollection RegisterServices(
        //    this IServiceCollection services)
        //{
        //    services.AddTransient<RegistrationsService>();
        //    services.AddTransient<AuthenticatedUserClient>();
        //    services.AddTransient<ISimpleUrlHelper, AspNetCoreSimpleUrlHelper>();
        //    services.AddScoped<VerifiedUserService>();
        //    services.AddScoped<ITimeProvider, UtcTimeProvider>();
        //    return services;
        //}

        public static IServiceCollection AddOuterApi(
            this IServiceCollection services,
            OuterApiConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddTransient<Http.MessageHandlers.DefaultHeadersHandler>();
            services.AddTransient<Http.MessageHandlers.LoggingMessageHandler>();
            services.AddTransient<Http.MessageHandlers.ApimHeadersHandler>();

            services
                .AddRestEaseClient<IOuterApiClient>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<Http.MessageHandlers.DefaultHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.ApimHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.LoggingMessageHandler>();

            services.AddTransient<IApimClientConfiguration>((_) => configuration);

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