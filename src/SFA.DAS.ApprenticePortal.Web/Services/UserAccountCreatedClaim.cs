using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public static class UserAccountCreatedClaim
    {
        public const string ClaimName = "AccountCreated";

        internal static ClaimsIdentity CreateAccountCreatedClaim()
            => new ClaimsIdentity(new[] { new Claim(ClaimName, "True") });
    }
}