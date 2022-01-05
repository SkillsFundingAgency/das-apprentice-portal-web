using FluentAssertions;
using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticePortal.UnitTests.FeaturesSteps
{
    [Binding]
    [Scope(Feature = "HomePage")]
    public class HomePageSteps : StepsBase
    {
        private readonly TestContext _context;
        private Apprentice _apprentice = An.Apprentice;

        public HomePageSteps(TestContext context) : base(context)
        {
            _context = context;
        }

        [Given(@"the apprentice is authenticated")]
        public void GivenTheApprenticeIsAuthenticated()
        {
            _context.Web.AuthoriseApprentice(_apprentice.ApprenticeId);
        }

        [Given(@"there is a single incomplete apprenticeship")]
        public void GivenThereIsASingleIncompleteApprenticeship()
        {
            _apprentice = _apprentice.WithApprenticeship(An.Apprenticeship);
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single confirmed apprenticeship")]
        public void GivenThereIsASingleConfirmedApprenticeship()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithConfirmedOn());
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single stopped apprenticeship")]
        public void GivenThereIsASingleStoppedApprenticeship()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithStoppedReceivedOn());
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single stopped apprenticeship which hasn't been viewed since being stopped")]
        public void GivenThereIsASingleStoppedApprenticeshipWhichHasnTBeenViewedSinceBeingStopped()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithLastViewedOn().FollowedByStoppedReceivedOn());
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single stopped apprenticeship and it has been viewed since being stopped")]
        public void GivenThereIsASingleStoppedApprenticeshipAndItHasBeenViewedSinceBeingStopped()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithStoppedReceivedOn().FollowedByViewedOn());
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [When(@"accessing the home page")]
        public async Task WhenAccessingTheHomePage()
        {
            await _context.Web.Get("home");
        }

        [Then(@"the response status should be Ok")]
        public void ThenTheResponseStatusShouldBeOk()
        {
            _context.Web.Response.Should().Be200Ok();
        }

        [Then(@"the apprenticeship status should show ""(.*)""")]
        public void ThenTheApprenticeshipStatusShouldShow(string status)
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.Status().ToString().ToUpper().Should().Be(status);
        }

        [Then(@"the employer name should be correct")]
        public void ThenTheEmployerNameShouldBeCorrect()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.EmployerName.Should().Be(_apprentice.Apprenticeship.EmployerName);
        }

        [Then(@"the course name should be correct")]
        public void ThenTheCourseNameShouldBeCorrect()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.CourseName.Should().Be(_apprentice.Apprenticeship.CourseName);
        }

        [Then(@"the just stopped information message should be visible")]
        public void ThenTheStoppedInformationMessageShouldBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.DisplayJustStoppedInfoMessage.Should().BeTrue();
        }

        [Then(@"the just stopped information message should not be visible")]
        public void ThenTheJustStoppedInformationMessageShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.DisplayJustStoppedInfoMessage.Should().BeFalse();
        }
    }
}