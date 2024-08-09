using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.Authentication.UnitTests.AutoFixtureCustomisations;

namespace SFA.DAS.ApprenticePortal.Authentication.UnitTests
{
    public class AuthenticationEventsTests
    {
        private HttpContext _httpContext;
        private Mock<IAuthenticationService> _authServiceMock;
        private Mock<IServiceProvider> _servicesProvider;
        private Mock<IApprenticeAccountProvider> _apprenticeAcountProvider;
        private AuthenticationEvents _sut;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void Initialise()
        {
            _httpContext = new DefaultHttpContext();
            _authServiceMock = new Mock<IAuthenticationService>();
            _servicesProvider = new Mock<IServiceProvider>();
            _apprenticeAcountProvider = new Mock<IApprenticeAccountProvider>();

            _servicesProvider.Setup(x => x.GetService(typeof(IAuthenticationService))).Returns(_authServiceMock.Object);
            _httpContext.RequestServices = _servicesProvider.Object;

            _sut = new AuthenticationEvents(_apprenticeAcountProvider.Object);
        }


        [TestCase(true)]
        [TestCase(false)]
        public async Task User_who_creates_a_new_account_should_have_these_claims(bool termsOfUse)
        {
            var identity = _fixture.Create<ClaimsPrincipal>();
            var apprentice = _fixture.Build<TestApprenticeAccount>().With(p => p.TermsOfUseAccepted, termsOfUse).Create();

            _authServiceMock.Setup(x=>x.AuthenticateAsync(It.IsAny<HttpContext>(), It.IsAny<string>())).ReturnsAsync(AuthenticateResult.Success(new AuthenticationTicket(identity, "test")));

            await AuthenticationEvents.UserAccountCreated(_httpContext, apprentice);

            identity.Claims.Should().Contain(c => c.Type.Equals(IdentityClaims.AccountCreated, StringComparison.OrdinalIgnoreCase));
            identity.Claims.Should().Contain(c => c.Type.Equals(IdentityClaims.GivenName, StringComparison.OrdinalIgnoreCase));
            identity.Claims.Should().Contain(c => c.Type.Equals(IdentityClaims.FamilyName, StringComparison.OrdinalIgnoreCase));
            if (termsOfUse)
            {
                identity.Claims.Should().Contain(c => c.Type.Equals(IdentityClaims.TermsOfUseAccepted, StringComparison.OrdinalIgnoreCase));
            }
        }


        [Test]
        public async Task User_who_updates_account_should_have_these_claims()
        {
            // Arrange
            var identity = _fixture.Create<ClaimsPrincipal>();
            var oldApprentice = _fixture.Create<TestApprenticeAccount>();
            _authServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<HttpContext>(), It.IsAny<string>())).ReturnsAsync(AuthenticateResult.Success(new AuthenticationTicket(identity, "test")));

            await AuthenticationEvents.UserAccountCreated(_httpContext, oldApprentice);

            var updatedApprentice = _fixture.Create<TestApprenticeAccount>();

            // Action
            await AuthenticationEvents.UserAccountUpdated(_httpContext, updatedApprentice);

            // Assert
            identity.Claims.Count(c => c.Type.Equals(IdentityClaims.GivenName, StringComparison.OrdinalIgnoreCase) && c.Value == updatedApprentice.FirstName).Should().Be(1);
            identity.Claims.Count(c => c.Type.Equals(IdentityClaims.FamilyName, StringComparison.OrdinalIgnoreCase) && c.Value == updatedApprentice.LastName).Should().Be(1);
            identity.Claims
                .Count(x => x.Type.Equals(IdentityClaims.AccountCreated, StringComparison.OrdinalIgnoreCase))
                .Should().Be(1);
        }


        [Test]
        public async Task User_who_confirms_terms_of_use_should_have_these_claims()
        {
            // Arrange
            var identity = _fixture.Create<ClaimsPrincipal>();
            _authServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<HttpContext>(), It.IsAny<string>())).ReturnsAsync(AuthenticateResult.Success(new AuthenticationTicket(identity, "test")));

            await AuthenticationEvents.TermsOfUseAccepted(_httpContext);
            await AuthenticationEvents.TermsOfUseAccepted(_httpContext);

            // Action
            await AuthenticationEvents.TermsOfUseAccepted(_httpContext);

            // Assert
            identity.Claims.Should().Contain(c => c.Type.Equals(IdentityClaims.TermsOfUseAccepted, StringComparison.OrdinalIgnoreCase));

            identity.Claims
                .Count(x => x.Type.Equals(IdentityClaims.TermsOfUseAccepted, StringComparison.OrdinalIgnoreCase))
                .Should().Be(1);
        }
    }
}