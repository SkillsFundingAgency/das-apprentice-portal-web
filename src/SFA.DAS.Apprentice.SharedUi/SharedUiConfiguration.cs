using SFA.DAS.Apprentice.SharedUi.GoogleAnalytics;
using SFA.DAS.Apprentice.SharedUi.Menu;
using SFA.DAS.Apprentice.SharedUi.Zendesk;

namespace SFA.DAS.Apprentice.SharedUi
{
    public interface ISharedUiConfiguration
    {
        public NavigationSectionUrls ApplicationUrls { get; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; }
        public ZenDeskConfiguration Zendesk { get; }
    }
}