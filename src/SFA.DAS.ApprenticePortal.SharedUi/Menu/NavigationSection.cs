using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public enum NavigationSection
    {
        Home,
        HelpAndSupport,
        ConfirmMyApprenticeship,
        ApprenticeAccounts,
        Login,
        PersonalDetails,
        TermsOfUse,
        Registration,
        ApprenticeFeedback,
        NotificationsSettings
    }

    public class NavigationSectionUrls
    {
        public Uri ApprenticeHomeUrl { get; set; } = null!;
        public Uri ApprenticeAccountsUrl { get; set; } = null!;
        public Uri ApprenticeCommitmentsUrl { get; set; } = null!;
        public Uri ApprenticeLoginUrl { get; set; } = null!;
        public Uri ApprenticeFeedbackUrl { get; set; } = null!;
        
        public Uri ToUri(NavigationSection section)
            => UriForSection(section)
                ?? throw new Exception(
                    $"URL for navigation section `{section}` is not configured");

        private Uri? UriForSection(NavigationSection section)
            => section switch
            {
                NavigationSection.Home => ApprenticeHomeUrl,
                NavigationSection.HelpAndSupport => ApprenticeHomeUrl,
                NavigationSection.ConfirmMyApprenticeship => ApprenticeCommitmentsUrl,
                NavigationSection.ApprenticeAccounts => ApprenticeAccountsUrl,
                NavigationSection.Login => ApprenticeLoginUrl,
                NavigationSection.PersonalDetails => new Uri(ApprenticeAccountsUrl, "Account"),
                NavigationSection.TermsOfUse => new Uri(ApprenticeAccountsUrl, "TermsOfUse"),
                NavigationSection.Registration => new Uri(ApprenticeCommitmentsUrl, "Register"),
                NavigationSection.ApprenticeFeedback => ApprenticeFeedbackUrl,
                _ => throw new Exception($"Unknown navigation section {section}"),
                NavigationSection.NotificationsSettings => new Uri(ApprenticeAccountsUrl, "NotificationsSettings")
            };
    }
}