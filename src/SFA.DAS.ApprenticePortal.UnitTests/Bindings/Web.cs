using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;
using SFA.DAS.ApprenticePortal.UnitTests.Hooks;
using SFA.DAS.ApprenticePortal.Web;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticePortal.UnitTests.Bindings
{
    [Binding]
    public class Web
    {
        private Fixture _fixture = new Fixture();
        public static HttpClient Client { get; set; }
        public CookieContainer Cookies { get; set; }
        public static Dictionary<string, string> Config { get; private set; }
        public static LocalWebApplicationFactory<ApplicationStartup> Factory { get; set; }
        public static Hook<IActionResult> ActionResultHook;

        private readonly TestContext _context;

        public Web(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario()]
        public void Initialise()
        {
            if (Client == null)
            {
                Config = new Dictionary<string, string>
                {
                    {"EnvironmentName", "ACCEPTANCE_TESTS"},
                    {"Authentication:MetadataAddress", _context.IdentityServiceUrl},
                    {"ApprenticeCommitmentsApi:ApiBaseUrl", _context.OuterApi?.BaseAddress ?? "https://api/"},
                    {"ApprenticeCommitmentsApi:SubscriptionKey", ""},
                    {"ApplicationUrls:ApprenticeHomeUrl", "https://home/"},
                    {"ApplicationUrls:ApprenticeAccountsUrl", "https://accounts/"},
                    {"ApplicationUrls:ApprenticeCommitmentsUrl", _context.OuterApi?.BaseAddress ?? "https://confirm/"},
                    {"ApplicationUrls:ApprenticeLoginUrl", _context.OuterApi?.BaseAddress ?? "https://login/"},
                };

                ActionResultHook = new Hook<IActionResult>();
                Factory = new LocalWebApplicationFactory<ApplicationStartup>(Config, ActionResultHook);
                var handler = new CookieContainerHandler()
                {
                    InnerHandler = Factory.Server.CreateHandler(),
                };
                Client = new HttpClient(handler) { BaseAddress = Factory.Server.BaseAddress };
                Cookies = handler.Container;
            }

            _context.Web = new ApprenticePortalWeb(Client, ActionResultHook, Config, Cookies);
            AuthenticationHandlerForTesting.Authentications.Clear();
        }

        [AfterScenario()]
        public void CleanUpScenario()
        {
            _context?.Web?.Dispose();
        }

        [AfterFeature()]
        public static void CleanUpFeature()
        {
            Client?.Dispose();
            Factory?.Dispose();
            Client = null;
        }
    }
}