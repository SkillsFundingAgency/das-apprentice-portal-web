using System;
using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Authentication.TestHelpers
{
    public static class TestUsers
    {
        public static AuthenticatedUser FakeLocalUserFullyVerified => new AuthenticatedUser(FakeLocalUserFullyVerifiedClaim(Guid.NewGuid()));
        public static AuthenticatedUser FakeLocalUserWithNoAccount => new AuthenticatedUser(FakeLocalUserWithNoAccountClaim(Guid.NewGuid()));
        public static AuthenticatedUser FakeLocalUserWithAccountButTermsOfUseNotAccepted => new AuthenticatedUser(FakeLocalUserWithAccountButTermsOfUseNotAcceptedClaim(Guid.NewGuid()));

        public static ClaimsPrincipal FakeLocalUserFullyVerifiedClaim(Guid apprenticeId)
        {
            return new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "lisa_simpson_esfa@mailinator.com"),
                    new Claim(IdentityClaims.GivenName, "Lisa"),
                    new Claim(IdentityClaims.FamilyName, "Simpson"),
                    new Claim(IdentityClaims.AccountCreated, "True"),
                    new Claim(IdentityClaims.TermsOfUseAccepted, "True"),
                    new Claim(IdentityClaims.VerifiedUser, "True"),
                })
            });
        }

        public static ClaimsPrincipal FakeLocalUserWithNoAccountClaim(Guid apprenticeId)
        {
            return new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "bart_simpson_esfa@mailinator.com"),
                })
            });
        }
        public static ClaimsPrincipal FakeLocalUserWithAccountButTermsOfUseNotAcceptedClaim(Guid apprenticeId)
        {
            return new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "homer_simpson_esfa@mailinator.com"),
                    new Claim(IdentityClaims.GivenName, "Homer"),
                    new Claim(IdentityClaims.FamilyName, "Simpson"),
                    new Claim(IdentityClaims.AccountCreated, "True")
                })
            });
        } 
    }
}