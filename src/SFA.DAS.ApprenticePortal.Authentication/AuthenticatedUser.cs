﻿using System;
using System.Net.Mail;
using System.Security.Claims;

namespace SFA.DAS.ApprenticePortal.Authentication
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

            var emailClaim = user.EmailAddressClaim()
                             ?? throw new InvalidOperationException($"There is no `{IdentityClaims.Name}` claim for the email.");

            Email = new MailAddress(emailClaim.Value ?? "");

            HasCreatedAccount = user.HasCreatedAccount();
            HasAcceptedTermsOfUse = user.HasAcceptedTermsOfUse();
        }

        public Guid ApprenticeId { get; }

        public MailAddress Email { get; }

        public bool HasCreatedAccount { get; }
        public bool HasAcceptedTermsOfUse { get; }

    }
}