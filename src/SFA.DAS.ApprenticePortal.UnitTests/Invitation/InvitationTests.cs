using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Web.Pages;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Web.Startup;

namespace SFA.DAS.ApprenticePortal.UnitTests.Invitation
{
    public class InvitationTests
    {
        [Test, PageAutoData]
        public void Accessing_index_without_invitation_forwards_to_confirm([Frozen] NavigationSectionUrls urls, IndexModel sut)
        {
            sut.Invitation = null;
            Assert.That(sut.StartUrl, Is.EqualTo($"{urls.ApprenticeCommitmentsUrl}register/{sut.Register}"));
        }

        [Test, PageAutoData]
        public void Accessing_index_without_invitation_forwards_to_login(
            [Frozen] NavigationSectionUrls urls, 
            [Frozen] Mock<ApplicationConfiguration> configuration,
            string invitation)
        {
            
            configuration.Setup(x => x.UseGovSignIn).Returns(false);
            var sut = new IndexModel(new NavigationUrlHelper(urls), configuration.Object)
            {
                Invitation = invitation
            };

            Assert.That(sut.StartUrl, Is.EqualTo($"{urls.ApprenticeLoginUrl}Invitations/CreatePassword/{invitation}"));
        }
        
        [Test, PageAutoData]
        public void Accessing_index_without_invitation_forwards_to_register_for_gov_sign_in(
            [Frozen] NavigationSectionUrls urls, 
            [Frozen] Mock<ApplicationConfiguration> configuration,
            string invitation)
        {
            
            configuration.Setup(x => x.UseGovSignIn).Returns(true);
            var indexModel = new IndexModel(new NavigationUrlHelper(urls), configuration.Object)
            {
                Invitation = invitation,
                Register = null
            };

            indexModel.StartUrl.Should().Be($"{urls.ApprenticeCommitmentsUrl}register/{indexModel.Register}");
        }
    }
}