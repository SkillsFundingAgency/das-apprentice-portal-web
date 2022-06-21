using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using System;
using System.Linq;
using System.Net.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock
{
    public class PortalOuterApiMock : IDisposable
    {
        private readonly WireMockServer _mock;

        public PortalOuterApiMock() : this(0, false) { }

        public PortalOuterApiMock(int? port = 0, bool ssl = false)
        {
            _mock = WireMockServer.Start(port, ssl);
            _mock.Given(Request.Create().WithPath("/hello")).RespondWith(Response.Create().WithSuccess());
        }

        public string BaseAddress => $"{_mock.Urls[0]}";

        public HttpClient HttpClient =>
            new HttpClient { BaseAddress = new Uri(BaseAddress) };

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
                    .WithPath($"/apprentices/{apprentice.ApprenticeUrlId()}").UsingGet())
                .AtPriority(9)
                .RespondWith(Response.Create()
                    .WithBodyAsJson(apprentice));

            _mock
                .Given(Request.Create()
                    .WithPath($"/apprentices/{apprentice.ApprenticeUrlId()}/homepage").UsingGet())
                .AtPriority(1)
                .RespondWith(Response.Create()
                    .WithBodyAsJson(homepage));

            return this;
        }

        internal PortalOuterApiMock WithApprentices(params Apprentice[] apprentices)
            => apprentices.Aggregate(this, (mock, apprentice) => mock.WithApprentice(apprentice));

        public void Dispose()
        {
        }
    }
}