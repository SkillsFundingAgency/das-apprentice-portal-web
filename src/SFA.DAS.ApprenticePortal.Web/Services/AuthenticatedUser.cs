using SFA.DAS.ApprenticePortal.SharedUi.Identity;
using System;
using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(ClaimsPrincipal user)
        {
            var claim = user.ApprenticeIdClaim()
                ?? throw new InvalidOperationException($"There is no `{IdentityClaims.ApprenticeId}` claim.");

            if (!Guid.TryParse(claim.Value, out var apprenticeId))
                throw new InvalidOperationException($"`{claim.Value}` in claim `{IdentityClaims.ApprenticeId}` is not a valid identifier");

            ApprenticeId = apprenticeId;
        }

        public Guid ApprenticeId { get; }
    }
}
