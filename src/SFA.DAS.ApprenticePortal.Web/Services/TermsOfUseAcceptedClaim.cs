using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public static class TermsOfUseAcceptedClaim
    {
        public const string ClaimName = "TermsOfUseAccepted";

        private static readonly Claim ClaimInstance = new Claim(ClaimName, "True");

        internal static ClaimsIdentity CreateTermsOfUseAcceptedClaim()
            => new ClaimsIdentity(new[] { ClaimInstance });
    }
}
