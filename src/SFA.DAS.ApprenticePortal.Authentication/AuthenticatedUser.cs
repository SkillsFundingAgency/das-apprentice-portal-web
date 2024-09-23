using System;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SFA.DAS.ApprenticePortal.Authentication
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext!.User;
            var claim = user.ApprenticeIdClaim();

            if (claim != null)
            {
                Guid.TryParse(claim.Value, out var apprenticeId);
                ApprenticeId = apprenticeId;    
            }
            else
            {
                ApprenticeId = Guid.Empty;
            }
            
            var emailClaim = user.EmailAddressClaim();

            Email = emailClaim?.Value != null ? new MailAddress(emailClaim?.Value ?? "") : null;

            HasCreatedAccount = user.HasCreatedAccount();
            HasAcceptedTermsOfUse = user.HasAcceptedTermsOfUse();
            HasFinishedAccountCreation = !string.IsNullOrEmpty(user.FullName());
        }

        public Guid ApprenticeId { get; }

        public MailAddress? Email { get; }

        public bool HasCreatedAccount { get; }
        public bool HasAcceptedTermsOfUse { get; }
        
        public bool HasFinishedAccountCreation { get; set; }
    }
}