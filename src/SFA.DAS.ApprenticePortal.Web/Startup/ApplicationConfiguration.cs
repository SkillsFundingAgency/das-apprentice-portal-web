﻿using SFA.DAS.ApprenticePortal.SharedUi;
using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;

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