using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public enum NavigationSection
    {
        Home,
        HelpAndSupport,
        ConfirmMyApprenticeship,
        MyApprenticeship,
        ApprenticeAccounts,
        Login,
        PersonalDetails,
        TermsOfUse,
        Registration,
        NotificationSettings,
        ApprenticeFeedback
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
                NavigationSection.MyApprenticeship => ApprenticeCommitmentsUrl,
                NavigationSection.ApprenticeAccounts => ApprenticeAccountsUrl,
                NavigationSection.Login => ApprenticeLoginUrl,
                NavigationSection.PersonalDetails => new Uri(ApprenticeAccountsUrl, "Account"),
                NavigationSection.TermsOfUse => new Uri(ApprenticeAccountsUrl, "TermsOfUse"),
                NavigationSection.Registration => new Uri(ApprenticeCommitmentsUrl, "Register"),
                NavigationSection.NotificationSettings => new Uri(ApprenticeAccountsUrl, "NotificationSettings"),
                NavigationSection.ApprenticeFeedback => ApprenticeFeedbackUrl,
                _ => throw new Exception($"Unknown navigation section {section}")
            };
    }
}