using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticePortal.Authentication.TestHelpers.TestHelpers;
using SFA.DAS.ApprenticePortal.UnitTests.Hooks;

namespace SFA.DAS.ApprenticePortal.UnitTests
{
    public class ApprenticePortalWeb : IDisposable
    {
        public HttpClient Client { get; }
        public HttpResponseMessage Response { get; set; }
        public Uri BaseAddress { get; }
        
        public IHook<IActionResult> ActionResultHook { get; set; }
        public Dictionary<string, string> Config { get; }
        public CookieContainer Cookies { get; }

        private bool isDisposed;

        public ApprenticePortalWeb(HttpClient client, IHook<IActionResult> actionResultHook, Dictionary<string, string> config, CookieContainer cookies)
        {
            Client = client;
            BaseAddress = client.BaseAddress;
            ActionResultHook = actionResultHook;
            Config = config;
            Cookies = cookies;
        }

        public void AuthoriseApprentice(Guid apprenticeId)
        {
            AuthenticationHandlerForTesting.AddUserWithFullAccount(apprenticeId);
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(apprenticeId.ToString());
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            Response?.Dispose();
            return Response = await Client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage message)
        {
            Response?.Dispose();
            return Response = await Client.SendAsync(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                Response?.Dispose();
            }

            isDisposed = true;
        }
    }
}