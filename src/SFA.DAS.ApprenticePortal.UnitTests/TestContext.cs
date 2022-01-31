using SFA.DAS.ApprenticePortal.OuterApi.Mock;
using SFA.DAS.ApprenticePortal.UnitTests.Hooks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticePortal.UnitTests
{
    public class TestContext
    {
        private readonly FeatureContext _feature;

        public TestContext(FeatureContext feature)
        {
            _feature = feature;
        }

        public ApprenticePortalWeb Web { get; set; }
        public PortalOuterApiMock OuterApi => _feature.GetOrAdd<PortalOuterApiMock>();
        public TestActionResult ActionResult { get; set; }
        public string IdentityServiceUrl => "https://identity";
    }
}