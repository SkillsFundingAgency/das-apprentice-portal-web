using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Identity
{
    public static class VerifiedUser
    {
        internal static async Task ConfirmIdentity(HttpContext context)
        {
            var authenticated = await context.AuthenticateAsync();

            if (authenticated.Succeeded)
            {
                var identity = (ClaimsIdentity)authenticated.Principal.Identity;
                identity.AddClaim(new Claim("VerifiedUser", "True"));
                await context.SignInAsync(authenticated.Principal, authenticated.Properties);
            }
        }

        internal static bool UserHasConfirmedIdentity(HttpContext httpContext)
            => httpContext.User.HasClaim("VerifiedUser", "True");

        internal static bool UserHasUnconfirmedIdentity(HttpContext httpContext)
            => !UserHasConfirmedIdentity(httpContext);
    }
}
