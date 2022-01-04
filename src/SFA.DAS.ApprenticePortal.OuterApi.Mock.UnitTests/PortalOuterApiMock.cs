using AutoFixture;
using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using System;
using System.Net.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock
{
    public class PortalOuterApiMock : IDisposable
    {
        private readonly WireMockServer _mock;
        private readonly Fixture _fixture = new Fixture();

        public PortalOuterApiMock()
        {
            _mock = WireMockServer.Start();
            _mock.Given(Request.Create().WithPath("/hello")).RespondWith(Response.Create().WithSuccess());
        }

        public HttpClient HttpClient =>
            new HttpClient { BaseAddress = new Uri($"{_mock.Urls[0]}") };

        public PortalOuterApiMock WithApprentice(Apprentice apprentice)
        {
            var homepage = new ApprenticeHomepage
            {
                Apprentice = apprentice,
            };

            if (apprentice.Apprenticeship != null)
                homepage.Apprenticeship = apprentice.Apprenticeship;

            _mock
                .Given(Request.Create()
                    .WithPath($"/apprentices/{apprentice.ApprenticeId}").UsingGet())
                .RespondWith(Response.Create()
                    .WithBodyAsJson(apprentice));

            _mock
               .Given(Request.Create()
                   .WithPath($"/apprentices/{apprentice.ApprenticeId}/homepage").UsingGet())
               .RespondWith(Response.Create()
                   .WithBodyAsJson(homepage));

            return this;
        }

        public void Dispose()
        {
        }
    }
}