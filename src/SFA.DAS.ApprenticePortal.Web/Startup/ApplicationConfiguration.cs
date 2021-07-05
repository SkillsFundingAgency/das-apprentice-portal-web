using SFA.DAS.Apprentice.SharedUi.GoogleAnalytics;
using SFA.DAS.Apprentice.SharedUi.Menu;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public class ApplicationConfiguration
    {
        public string ApprenticeCommitmentsBaseUrl { get; set; }
        public NavigationSectionUrls ApplicationUrls { get; set; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; set; }
    }
}