using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Startup;
using SFA.DAS.Encoding;
using SFA.DAS.GovUK.Auth.AppStart;
using SFA.DAS.GovUK.Auth.Services;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public class ApplicationStartup
    {
        public ApplicationStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = Configuration.Get<ApplicationConfiguration>();
            var encodingsConfig = Configuration.Get<EncodingConfig>();
            services.AddSingleton(appConfig);

            services.AddTransient(_ => new NavigationUrlHelper(appConfig.ApplicationUrls));

            services
                .AddTelemetryRegistration(Configuration)
                .AddApplicationInsightsTelemetry();

            services.AddDataProtection(appConfig.ConnectionStrings, Environment);

            services.EnableGoogleAnalytics(appConfig.GoogleAnalytics);
            services.AddHealthChecks();
            services.AddTransient<ICustomClaims, ApprenticeAccountPostAuthenticationClaimsHandler>();
            if (appConfig.UseGovSignIn)
            {
                services.AddGovLoginAuthentication(appConfig.ApplicationUrls,Configuration);
            }
            else
            {
                services.AddTransient<IOidcService, StubOidcService>();
                services.AddAuthentication(appConfig.ApplicationUrls, Environment);    
            }
            
            services.AddOuterApi(appConfig.ApprenticePortalOuterApi);
            services.AddServices();
            services.AddRazorPages();
            services.AddSharedUi(appConfig, options =>
            {
                options.EnableZendesk();
                options.EnableGoogleAnalytics();
                options.SetCurrentNavigationSection(NavigationSection.Home);
                options.SetUseGovSignIn(appConfig.UseGovSignIn);
            });
            services.AddSingleton<IEncodingService>( new EncodingService(encodingsConfig));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHealthChecks("/ping");

            app.UseRouting();
            app.UseSharedUi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}