using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public enum NavigationSection
    {
        Home,
        HelpAndSupport,
        ConfirmMyApprenticeship,
        Login,
    }

    public class NavigationSectionUrls
    {
        public Uri ApprenticeHomeUrl { get; set; } = null!;
        public Uri ApprenticeCommitmentsUrl { get; set; } = null!;
        public Uri ApprenticeLoginUrl { get; set; } = null!;

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
                NavigationSection.Login => ApprenticeLoginUrl,
                _ => throw new Exception($"Unknown nagivation section {section}")
            };
    }
}