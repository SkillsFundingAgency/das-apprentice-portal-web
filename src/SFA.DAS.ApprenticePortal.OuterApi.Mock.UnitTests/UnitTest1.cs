using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.UnitTests
{
    public class Tests
    {
        [Test]
        public async Task GetHello()
        {
            using var mock = new PortalOuterApiMock();
            using var response = await mock.HttpClient.GetAsync("/hello");
            response.Should().Be2XXSuccessful();
        }

        [Test, AutoData]
        public async Task Return_a_simple_apprentice(Apprentice apprentice)
        {
            using var mock = new PortalOuterApiMock();
            mock.WithApprentice(apprentice);

            using var response = await mock.HttpClient.GetAsync($"/apprentices/{apprentice.ApprenticeId}");
            response.Should().Be2XXSuccessful();
        }

        [Test, AutoData]
        public async Task Respond_with_404_for_an_unspecified_apprentice(Guid apprenticeId)
        {
            using var mock = new PortalOuterApiMock();
            using var response = await mock.HttpClient.GetAsync($"/apprentices/{apprenticeId}");
            response.Should().Be404NotFound();
        }

        [Test, AutoData]
        public async Task Return_a_null_Apprenticeship_for_Apprentice_WithoutApprenticeship(Guid id)
        {
            using var mock = new PortalOuterApiMock()
                .WithApprentice(Natural.Apprentice
                    .WithId(id)
                    .WithoutApprenticeship());

            using var response = await mock.HttpClient.GetAsync($"/apprentices/{id}/homepage");

            response.Should().Be2XXSuccessful().And.BeAs(new
            {
                Apprentice = new { ApprenticeId = id },
                Apprenticeship = (Apprenticeship?)null,
            });
        }

        [Test, AutoData]
        public async Task Return_Apprentice_and_Apprenticeship_as_specified(Apprentice apprentice, Apprenticeship apprenticeship)
        {
            apprenticeship = apprenticeship.ForApprentice(apprentice);

            using var mock = new PortalOuterApiMock()
                .WithApprentice(apprentice.WithApprenticeship(apprenticeship));

            using var response = await mock.HttpClient.GetAsync($"/apprentices/{apprentice.ApprenticeId}/homepage");

            response.Should().Be2XXSuccessful().And.BeAs(new
            {
                Apprentice = apprentice,
                Apprenticeship = apprenticeship,
            });
        }

        [Test, AutoData]
        public async Task WithConfirmed_creates_a_random_confirmed_datetime(Apprentice apprentice)
        {
            var apprenticeship = Natural.Apprenticeship.WithConfirmed();

            using var mock = new PortalOuterApiMock()
                .WithApprentice(apprentice.WithApprenticeship(apprenticeship));

            using var response = await mock.HttpClient.GetAsync($"/apprentices/{apprentice.ApprenticeId}/homepage");

            response.Should().Be2XXSuccessful().And.Satisfy<ApprenticeHomepage>(homepage =>
                homepage.Apprenticeship.ConfirmedOn.Should().Be(apprenticeship.ConfirmedOn));
        }

        [Test, AutoData]
        public async Task Apprenticeship_can_create_dates_in_predictable_sequence(Guid id, DateTime confirmedOn)
        {
            using var mock = new PortalOuterApiMock()
                .WithApprentice(Natural.Apprentice
                    .WithId(id)
                    .WithApprenticeship(Natural.Apprenticeship
                        .WithConfirmed(confirmedOn)
                        .FollowedByStopped()
                        .FollowedByViewed()));

            using var response = await mock.HttpClient.GetAsync($"/apprentices/{id}/homepage");

            response.Should().Be2XXSuccessful().And.BeAs(new
            {
                Apprentice = new
                {
                    ApprenticeId = id,
                },
                Apprenticeship = new
                {
                    ApprenticeId = id,
                    ConfirmedOn = confirmedOn,
                    StoppedReceivedOn = confirmedOn.AddDays(1),
                    LastViewed = confirmedOn.AddDays(2),
                }
            });
        }
    }
}