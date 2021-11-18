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
        public MockApi OuterApi => _feature.GetOrAdd<MockApi>();
        public TestActionResult ActionResult { get; set; }
        public string IdentityServiceUrl => "https://identity";
    }
}
