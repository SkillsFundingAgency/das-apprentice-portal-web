﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Web.Services;
using System.IdentityModel.Tokens.Jwt;

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

        private static IServiceCollection AddApplicationAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IWebHostEnvironment environment)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            IdentityModelEventSource.ShowPII = true;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = ".Apprenticeships.Application";
                    //options.Cookie.HttpOnly = true;
                    //options.Cookie.SameSite = SameSiteMode.Lax;
                    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = System.TimeSpan.FromHours(1);
                    //options.CookieManager = new ChunkingCookieManager();

                    if (environment.EnvironmentName != "Development")
                    {
                        options.Cookie.Domain = ".apprenticeships.education.gov.uk";
                    }
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = config.ApprenticeLoginUrl.ToString();
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "apprentice";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");

                    options.SaveTokens = true;
                    options.DisableTelemetry = false;

                    options.EventsType = typeof(AuthenticationEvents);
                });

            services.AddScoped<AuthenticationEvents>();
            
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