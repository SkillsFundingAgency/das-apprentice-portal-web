using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;

namespace SFA.DAS.ApprenticePortal.SharedUi
{
    public interface ISharedUiConfiguration
    {
        public NavigationSectionUrls ApplicationUrls { get; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; }
        public ZenDeskConfiguration Zendesk { get; }
    }
}