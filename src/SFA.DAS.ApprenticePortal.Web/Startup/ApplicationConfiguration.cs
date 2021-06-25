using SFA.DAS.Apprentice.SharedUi.GoogleAnalytics;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public class ApplicationConfiguration
    {
        public string ApprenticeCommitmentsBaseUrl { get; set; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; set; }
    }
}