using System.Linq;
using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Authentication
{
    public static class IdentityClaims
    {
        public const string ApprenticeId = "apprentice_id";
        public const string LogonId = "sub";
        public const string Name = "name"; // this is email address
        public const string GivenName = "given_name";
        public const string FamilyName = "family_name";
        public const string AccountCreated = "AccountCreated";
        public const string TermsOfUseAccepted = "TermsOfUseAccepted";
        public const string VerifiedUser = "VerifiedUser";

        public static Claim? ApprenticeIdClaim(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ApprenticeId);

        public static Claim? EmailAddressClaim(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(x => IsName(x));

        public static string FullName(this ClaimsPrincipal user)
        {
            var first = user.Claims.FirstOrDefault(x => IsGivenName(x))?.Value ?? "";
            var last = user.Claims.FirstOrDefault(x => IsFamilyName(x))?.Value ?? "";
            return $"{first} {last}".Trim();
        }

        private static bool IsName(Claim x)
            => x.Type == Name || x.Type == ClaimTypes.Name;

        private static bool IsGivenName(Claim x)
            => x.Type == GivenName || x.Type == ClaimTypes.GivenName;

        private static bool IsFamilyName(Claim x)
            => x.Type == FamilyName || x.Type == ClaimTypes.Surname;

        public static void AddAccountCreatedClaim(this ClaimsPrincipal principal)
        {
            var claimIdentity = new ClaimsIdentity(new[] {new Claim(AccountCreated, "True")});
            principal.AddIdentity(claimIdentity);
        }

        public static void AddTermsOfUseAcceptedClaim(this ClaimsPrincipal principal)
        {
            var claimIdentity = new ClaimsIdentity(new[] { new Claim(TermsOfUseAccepted, "True") });
            principal.AddIdentity(claimIdentity);
        }

        public static void AddApprenticeIdClaim(this ClaimsPrincipal principal, string id)
        {
            var claimIdentity = new ClaimsIdentity(new[] { new Claim(ApprenticeId, id) });
            principal.AddIdentity(claimIdentity);
        }

        public static void AddVerifiedUserClaim(this ClaimsPrincipal principal, string id)
        {
            var claimIdentity = new ClaimsIdentity(new[] { new Claim(VerifiedUser, "True") });
            principal.AddIdentity(claimIdentity);
        }

        internal static bool HasCreatedAccount(this ClaimsPrincipal principal)
            => principal.HasClaim(AccountCreated, "True");

        internal static bool HasAcceptedTermsOfUse(this ClaimsPrincipal principal)
            => principal.HasClaim(TermsOfUseAccepted, "True");

        internal static bool HasBeenVerified(this ClaimsPrincipal principal)
            => principal.HasClaim(VerifiedUser, "True");
    }
}