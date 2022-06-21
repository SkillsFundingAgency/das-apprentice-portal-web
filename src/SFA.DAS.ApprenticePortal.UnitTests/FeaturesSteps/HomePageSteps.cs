using FluentAssertions;
using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using SFA.DAS.ApprenticePortal.SharedUi.Home;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Pages;
using System;
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
                An.Apprenticeship.WithConfirmedOn().WithHasBeenConfirmedAtLeastOnce(true));
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single unconfirmed apprenticeship which had previously been confirmed")]
        public void GivenThereIsASingleUnconfirmedApprenticeshipWhichHadPreviouslyBeenConfirmed()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithHasBeenConfirmedAtLeastOnce(true));
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

        [Given(@"there is a single stopped apprenticeship which hasn't ever been viewed")]
        public void GivenThereIsASingleStoppedApprenticeshipWhichHasntEverBeenViewed()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithStoppedReceivedOn(DateTime.Now));
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is a single stopped apprenticeship and it has been viewed since being stopped")]
        public void GivenThereIsASingleStoppedApprenticeshipAndItHasBeenViewedSinceBeingStopped()
        {
            _apprentice = _apprentice.WithApprenticeship(
                An.Apprenticeship.WithStoppedReceivedOn().FollowedByViewedOn());
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [Given(@"there is an unmatched account")]
        public void GivenThereIsAnUnmatchedAccount()
        {
            _context.OuterApi.WithApprentice(_apprentice);
        }

        [When(@"accessing the home page")]
        public async Task WhenAccessingTheHomePage()
        {
            await _context.Web.Get("home");
        }

        [When(@"accessing the home page with the notification ""(.*)""")]
        public async Task WhenAccessingTheHomePageWithTheNotification(string notification)
        {
            await _context.Web.Get($"home?notification={notification}");
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

        [Then(@"the My Apprenticeship card should be visible")]
        public void ThenTheMyApprenticeshipCardShouldBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.ShowMyApprenticeshipCard.Should().BeTrue();
        }

        [Then(@"the My Apprenticeship card should not be visible")]
        public void ThenTheMyApprenticeshipCardShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.ShowMyApprenticeshipCard.Should().BeFalse();
        }

        [Then(@"the Confirm My Apprenticeship card should not be visible")]
        public void ThenTheConfirmMyApprenticeshipCardShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.ShowConfirmMyApprenticeshipCard.Should().BeFalse();
        }

        [Then(@"the Confirm My Apprenticeship card should be visible")]
        public void ThenTheConfirmMyApprenticeshipCardShouldBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.ShowConfirmMyApprenticeshipCard.Should().BeTrue();
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
                .Which.HomePageModel.Notification.Should().Be(HomeNotification.ApprenticeshipStopped);
        }

        [Then(@"the notification should be ""(.*)""")]
        public void ThenTheNotificationShouldBe(string notification)
        {
            Enum.TryParse<HomeNotification>(notification, out var notificationEnum);

            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.Notification.Should().Be(notificationEnum);
        }

        [Then(@"the just stopped information message should not be visible")]
        public void ThenTheJustStoppedInformationMessageShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.Notification.Should().NotBe(HomeNotification.ApprenticeshipStopped);
        }

        [Then(@"the apprenticeship matched information messages should not be visible")]
        public void ThenTheApprenticeshipMatchedInformationMessagesShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>()
                .Which.HomePageModel.Notification.Should().NotBe(HomeNotification.ApprenticeshipMatched);
        }
    }
}