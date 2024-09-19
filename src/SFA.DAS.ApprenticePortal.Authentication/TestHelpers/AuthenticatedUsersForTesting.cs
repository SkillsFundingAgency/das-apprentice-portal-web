using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;

namespace SFA.DAS.ApprenticePortal.Authentication.TestHelpers
{
    public static class AuthenticatedUsersForTesting
    {
        public static AuthenticatedUser FakeLocalUserFullyVerified => new AuthenticatedUser(FakeLocalUserFullyVerifiedClaim(Guid.NewGuid()));
        public static AuthenticatedUser FakeLocalUserWithNoAccount => new AuthenticatedUser(FakeLocalUserWithNoAccountClaim(Guid.NewGuid()));
        public static AuthenticatedUser FakeLocalUserWithAccountButTermsOfUseNotAccepted => new AuthenticatedUser(FakeLocalUserWithAccountButTermsOfUseNotAcceptedClaim(Guid.NewGuid()));

        public static IHttpContextAccessor FakeLocalUserFullyVerifiedClaim(Guid apprenticeId)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            
            var principal = new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "lisa_simpson_esfa@mailinator.com"),
                }, "Test")
            });

            principal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(IdentityClaims.GivenName, "Lisa"),
                new Claim(IdentityClaims.FamilyName, "Simpson"),
                new Claim(IdentityClaims.AccountCreated, "True"),
                new Claim(IdentityClaims.TermsOfUseAccepted, "True"),
                new Claim(IdentityClaims.VerifiedUser, "True"),
            }, "TestAdditional"));

            var defaultHttpContext = new DefaultHttpContext()
            {
                User = principal
            };

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(defaultHttpContext);
            
            return mockHttpContextAccessor.Object;
        }

        public static IHttpContextAccessor FakeLocalUserWithNoAccountClaim(Guid apprenticeId)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var principal =  new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "bart_simpson_esfa@mailinator.com"),
                }, "Test")
            });
            
            var defaultHttpContext = new DefaultHttpContext()
            {
                User = principal
            };

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(defaultHttpContext);
            
            return mockHttpContextAccessor.Object;
        }
        public static IHttpContextAccessor FakeLocalUserWithAccountButTermsOfUseNotAcceptedClaim(Guid apprenticeId)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var principal = new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "homer_simpson_esfa@mailinator.com"),
                }, "Test")
            });

            principal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(IdentityClaims.GivenName, "Lisa"),
                new Claim(IdentityClaims.FamilyName, "Simpson"),
                new Claim(IdentityClaims.GivenName, "Homer"),
                new Claim(IdentityClaims.FamilyName, "Simpson"),
                new Claim(IdentityClaims.AccountCreated, "True")
            }, "TestAdditional"));

            var defaultHttpContext = new DefaultHttpContext()
            {
                User = principal
            };

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(defaultHttpContext);
            
            return mockHttpContextAccessor.Object;

        }
    }
}