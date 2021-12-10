﻿using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Authentication;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class AuthenticationEvents : OpenIdConnectEvents
    {
        private readonly ApprenticeService _client;

        public AuthenticationEvents(ApprenticeService client) => _client = client;

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            await base.TokenValidated(context);
            await AddClaims(context.Principal);
        }

        public async Task AddClaims(ClaimsPrincipal principal)
        {
            var apprentice = await GetApprentice(principal);
            if (apprentice == null) return;

            AddAccountCreatedClaim(principal);
            AddTermsOfUseClaim(principal);
            AddApprenticeNameClaims(apprentice, principal);
        }

        private static void AddAccountCreatedClaim(ClaimsPrincipal principal)
            => principal.AddIdentity(UserAccountCreatedClaim.CreateAccountCreatedClaim());

        private static void AddTermsOfUseClaim(ClaimsPrincipal principal)
            => principal.AddIdentity(TermsOfUseAcceptedClaim.CreateTermsOfUseAcceptedClaim());

        private async Task<Apprentice?> GetApprentice(ClaimsPrincipal principal)
        {
            var claim = principal.ApprenticeIdClaim();

            if (claim == null) return null;
            if (!Guid.TryParse(claim.Value, out var apprenticeId)) return null;

            return await _client.TryGetApprentice(apprenticeId);
        }

        private void AddApprenticeNameClaims(Apprentice apprentice, ClaimsPrincipal principal)
            => principal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(IdentityClaims.GivenName, apprentice.FirstName),
                new Claim(IdentityClaims.FamilyName, apprentice.LastName),
            }));
    }
}
