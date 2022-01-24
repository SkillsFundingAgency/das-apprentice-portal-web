using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Pages;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace SFA.DAS.ApprenticePortal.UnitTests.FeaturesSteps
{
    [Binding]
    [Scope(Feature = "HomePage")]
    public class HomePageSteps : StepsBase
    {
        private TestContext _context;
        private Guid _apprenticeId;
        private Apprenticeship _singleApprenticeship;
        private ApprenticeHomepage _apprenticeHomepage;
        private Fixture _fixture;

        public HomePageSteps(TestContext context) : base(context)
        {
            _fixture = new Fixture();
            _context = context;
            _apprenticeId = Guid.NewGuid();

            _apprenticeHomepage = new ApprenticeHomepage();
            _apprenticeHomepage.Apprentice = new Apprentice() { ApprenticeId = _apprenticeId };
        }

        [Given(@"the apprentice is authenticated")]
        public void GivenTheApprenticeIsAuthenticated()
        {
            _context.Web.AuthoriseApprentice(_apprenticeId); 
        }

        [Given(@"there is a single incomplete apprenticeship")]
        public void GivenThereIsASingleIncompleteApprenticeship()
        {
            _singleApprenticeship = _fixture.Build<Apprenticeship>().With(x => x.IsStopped, false)
                .Without(x => x.StoppedReceivedOn).Without(x => x.ConfirmedOn).Create();

            OuterApiToReturnTheseApprenticeships(new Apprenticeship[] { _singleApprenticeship });
        }

        [Given(@"there is a single confirmed apprenticeship")]
        public void GivenThereIsASingleConfirmedApprenticeship()
        {
            _singleApprenticeship = _fixture.Build<Apprenticeship>().With(x => x.IsStopped, false)
                .Without(x => x.StoppedReceivedOn).With(x => x.ConfirmedOn, DateTime.Now).Create();

            OuterApiToReturnTheseApprenticeships(new Apprenticeship[] { _singleApprenticeship });
        }

        [Given(@"there is a single stopped apprenticeship")]
        public void GivenThereIsASingleStoppedApprenticeship()
        {
            _singleApprenticeship = _fixture.Build<Apprenticeship>().With(x => x.IsStopped, true)
                .With(x => x.StoppedReceivedOn, DateTime.Now).Create();

            OuterApiToReturnTheseApprenticeships(new Apprenticeship[] { _singleApprenticeship });
        }

        [Given(@"there is a single stopped apprenticeship which hasn't been viewed since being stopped")]
        public void GivenThereIsASingleStoppedApprenticeshipWhichHasnTBeenViewedSinceBeingStopped()
        {
            _singleApprenticeship = _fixture.Build<Apprenticeship>().With(x => x.IsStopped, true)
                .With(x => x.StoppedReceivedOn, DateTime.Now).With(x=>x.LastViewed, DateTime.Now.AddMonths(-1)).Create();

            OuterApiToReturnTheseApprenticeships(new Apprenticeship[] { _singleApprenticeship });
        }

        [Given(@"there is a single stopped apprenticeship and it has been viewed since being stopped")]
        public void GivenThereIsASingleStoppedApprenticeshipAndItHasBeenViewedSinceBeingStopped()
        {
            _singleApprenticeship = _fixture.Build<Apprenticeship>().With(x => x.IsStopped, true)
                .With(x => x.StoppedReceivedOn, DateTime.Now.AddDays(-1)).With(x => x.LastViewed, DateTime.Now).Create();

            OuterApiToReturnTheseApprenticeships(new Apprenticeship[] { _singleApprenticeship });
        }

        [When(@"accessing the home page")]
        public async Task WhenAccessingTheHomePage()
        {
            await _context.Web.Get("home");
        }

        [Then(@"the response status should be Ok")]
        public void ThenTheResponseStatusShouldBeOk()
        {
            _context.Web.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the apprenticeship status should show ""(.*)""")]
        public void ThenTheApprenticeshipStatusShouldShow(string status)
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>().Which.HomePageModel.Status().ToString().ToUpper().Should().Be(status);
        }

        [Then(@"the employer name should be correct")]
        public void ThenTheEmployerNameShouldBeCorrect()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>().Which.HomePageModel.EmployerName.Should().Be(_singleApprenticeship.EmployerName);
        }

        [Then(@"the course name should be correct")]
        public void ThenTheCourseNameShouldBeCorrect()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>().Which.HomePageModel.CourseName.Should().Be(_singleApprenticeship.CourseName);
        }

        [Then(@"the just stopped information message should be visible")]
        public void ThenTheStoppedInformationMessageShouldBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>().Which.HomePageModel.DisplayJustStoppedInfoMessage.Should().BeTrue();
        }

        [Then(@"the just stopped information message should not be visible")]
        public void ThenTheJustStoppedInformationMessageShouldNotBeVisible()
        {
            _context.ActionResult.LastPageResult.Model.Should().BeOfType<HomeModel>().Which.HomePageModel.DisplayJustStoppedInfoMessage.Should().BeFalse();
        }

        private void OuterApiToReturnTheseApprenticeships(Apprenticeship[] apprenticeships)
        {
            _apprenticeHomepage.Apprenticeship = _singleApprenticeship;

            _context.OuterApi.MockServer.Given(
                    Request.Create()
                        .UsingGet()
                        .WithPath($"/apprentices/{_apprenticeId}/homepage"))
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(_apprenticeHomepage));
        }
    }
}