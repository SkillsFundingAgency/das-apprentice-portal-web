using SFA.DAS.ApprenticePortal.UnitTests.Hooks;

namespace SFA.DAS.ApprenticePortal.UnitTests
{
    public class TestContext 
    {
        public ApprenticePortalWeb Web { get; set; }
        public MockApi OuterApi { get; set; }
        public TestActionResult ActionResult { get; set; }
        public string IdentityServiceUrl { get; } = "https://identity";
    }
}
