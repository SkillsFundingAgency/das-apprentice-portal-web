using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.GovUK.Auth.AppStart;
using SFA.DAS.GovUK.Auth.Models;

namespace SFA.DAS.ApprenticePortal.Authentication
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This is the registration service for the ApprenticeAuthentication Service. To work it does require the IApprenticeAccountProvider interface
        /// to be implemented in the consuming service. The apprentice account provider allows us to populate the apprentice account claims. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static IServiceCollection AddApprenticeAuthentication(this IServiceCollection services,
            string metadataAddress,
            IWebHostEnvironment environment)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = ".Apprenticeships.Application";
                    options.Cookie.HttpOnly = true;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = System.TimeSpan.FromHours(1);
                    if (environment.EnvironmentName != "Development")
                        options.Cookie.Domain = ".apprenticeships.education.gov.uk";
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = metadataAddress;
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

        public static void AddGovLoginAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var cookieDomain = DomainExtensions.GetDomain(configuration["ResourceEnvironmentName"]);
            var loginRedirect = string.IsNullOrEmpty(cookieDomain)? "" : $"https://confirm.{cookieDomain}/account-details";
            var signedOutRedirectUrl = string.IsNullOrEmpty(cookieDomain) ? "https://localhost:5001/apprentice-signed-out" : $"https://{cookieDomain}/apprentice-signed-out";
            var localStubLoginPath = "/account-details";
            services.AddAndConfigureGovUkAuthentication(configuration,
                new AuthRedirects
                {
                    CookieDomain = cookieDomain,
                    LocalStubLoginPath = localStubLoginPath,
                    LoginRedirect = loginRedirect,
                    SignedOutRedirectUrl = signedOutRedirectUrl
                },
                typeof(ApprenticeAccountPostAuthenticationClaimsHandler));
            
            services.AddHttpContextAccessor();
        }
    }
}