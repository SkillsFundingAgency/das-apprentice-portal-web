using SFA.DAS.ApprenticePortal.SharedUi;
using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public class ApplicationConfiguration : ISharedUiConfiguration
    {
        public NavigationSectionUrls ApplicationUrls { get; set; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; set; }
        public ZenDeskConfiguration Zendesk { get; set; }
        public OuterApiConfiguration ApprenticeCommitmentsApi { get; set; }
        public DataProtectionConnectionStrings ConnectionStrings { get; set; }
    }
}