﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.Web.Services;
using SFA.DAS.GovUK.Auth.AppStart;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public static class AuthenticationStartup
    {
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IWebHostEnvironment environment)
        {           
            services
                .AddApplicationAuthentication(config, environment)
                .AddApplicationAuthorisation();

            services.AddTransient((_) => config);

            return services;
        }

        public static void AddGovLoginAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IConfiguration configuration)
        {
            services.AddGovLoginAuthentication(configuration);
            services.AddTransient<IApprenticeAccountProvider, ApprenticeAccountProvider>();
            services.AddAuthorization();
            services.AddScoped<AuthenticatedUser>();
            services.AddHttpContextAccessor();
            services.AddTransient((_) => config);
        }

        private static IServiceCollection AddApplicationAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IWebHostEnvironment environment)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddApprenticeAuthentication(config.ApprenticeLoginUrl.ToString(), environment);
            services.AddTransient<IApprenticeAccountProvider, ApprenticeAccountProvider>();
            return services;
        }

        private static IServiceCollection AddApplicationAuthorisation(
            this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddScoped<AuthenticatedUser>();
            services.AddScoped(s => s
                .GetRequiredService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}