using System;

namespace SFA.DAS.ApprenticePortal.Authentication
{
    public interface IApprenticeAccount
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool TermsOfUseAccepted { get; set; }
    }
}