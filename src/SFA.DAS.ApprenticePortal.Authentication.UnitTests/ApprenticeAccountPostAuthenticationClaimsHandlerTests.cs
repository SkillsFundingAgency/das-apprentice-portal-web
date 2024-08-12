using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.Authentication.UnitTests.AutoFixtureCustomisations;

namespace SFA.DAS.ApprenticePortal.Authentication.UnitTests;

public class ApprenticeAccountPostAuthenticationClaimsHandlerTests
{
    [Test, MoqAutoData]
    public async Task Then_The_Api_Is_Called_And_Claims_Set(
        string nameIdentifier,
        string emailAddress,
        TestApprenticeAccount apprenticeAccount,
        [Frozen] Mock<IApprenticeAccountProvider> apprenticeAccountProvider,
        ApprenticeAccountPostAuthenticationClaimsHandler handler)
    {
        apprenticeAccount.TermsOfUseAccepted = true;
        var tokenValidatedContext = ArrangeTokenValidatedContext(nameIdentifier, emailAddress);
        apprenticeAccountProvider.Setup(x =>
                x.PutApprenticeAccount(emailAddress, nameIdentifier)).ReturnsAsync(apprenticeAccount);
        
        var actual = await handler.GetClaims(tokenValidatedContext);
        
        actual.First(c => c.Type.Equals(IdentityClaims.GivenName)).Value.Should().Be(apprenticeAccount.FirstName);
        actual.First(c => c.Type.Equals(IdentityClaims.FamilyName)).Value.Should().Be(apprenticeAccount.LastName);
        actual.First(c => c.Type.Equals(IdentityClaims.TermsOfUseAccepted)).Value.Should().Be("True");
        actual.First(c => c.Type.Equals(IdentityClaims.ApprenticeId)).Value.Should().Be(apprenticeAccount.ApprenticeId.ToString());
    }
    
    private TokenValidatedContext ArrangeTokenValidatedContext(string nameIdentifier, string emailAddress)
    {
        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
            new Claim(ClaimTypes.Email, emailAddress)
        });
        
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(identity));
        return new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",","", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), Mock.Of<ClaimsPrincipal>(), new AuthenticationProperties())
        {
            Principal = claimsPrincipal
        };
    }
    
    private class TestAuthHandler : IAuthenticationHandler
    {
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }
    }
}