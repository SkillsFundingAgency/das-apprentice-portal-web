using SFA.DAS.Apprentice.SharedUi;
using SFA.DAS.Apprentice.SharedUi.GoogleAnalytics;
using SFA.DAS.Apprentice.SharedUi.Menu;
using SFA.DAS.Apprentice.SharedUi.Zendesk;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public class ApplicationConfiguration : ISharedUiConfiguration
    {
        public string ApprenticeCommitmentsBaseUrl { get; set; }
        public NavigationSectionUrls ApplicationUrls { get; set; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; set; }
        public ZenDeskConfiguration Zendesk { get; set; }
    }
}