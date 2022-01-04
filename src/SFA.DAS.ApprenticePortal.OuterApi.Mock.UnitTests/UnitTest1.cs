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
        public void Should()
        {
            true.Should().BeTrue();
        }

        [Test]
        public async Task GetHello()
        {
            using var mock = new PortalOuterApiMock();

            using var client = mock.HttpClient;
            using var response = await client.GetAsync("/hello");
            response.Should().Be2XXSuccessful();
        }

        [Test, AutoData]
        public async Task GetApprentice(Apprentice apprentice)
        {
            using var mock = new PortalOuterApiMock();
            mock.WithApprentice(apprentice);

            using var client = mock.HttpClient;
            using var response = await client.GetAsync($"/apprentices/{apprentice.ApprenticeId}");
            response.Should().Be2XXSuccessful();
        }

        [Test, AutoData]
        public async Task Can_chain_apprenticeship_actions3(Guid id, DateTime confirmedOn)
        {
            using var mock = new PortalOuterApiMock()
                .WithApprentice(Natural.Apprentice
                    .WithId(id)
                    .WithApprenticeship(Natural.Apprenticeship
                        .WithConfirmed(confirmedOn)
                        .FollowedByStopped()
                        .FollowedByViewed()));

            using var client = mock.HttpClient;
            using var response = await client.GetAsync($"/apprentices/{id}/homepage");

            response.Should().Be2XXSuccessful();
            response.Should().BeAs(new
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