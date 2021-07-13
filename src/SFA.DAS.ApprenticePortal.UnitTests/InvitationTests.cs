using AutoFixture.NUnit3;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Web.Pages;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticePortal.UnitTests
{
    public class InvitationTests
    {
        [Test, PageAutoData]
        public void Accessing_index_without_invitation_forwards_to_confirm([Frozen] NavigationSectionUrls urls, IndexModel sut)
        {
            sut.Invitation = null;
            Assert.That(sut.StartUrl, Is.EqualTo($"{urls.ApprenticeCommitmentsUrl}apprenticeships"));
        }

        [Test, PageAutoData]
        public void Accessing_index_without_invitation_forwards_to_login([Frozen] NavigationSectionUrls urls, IndexModel sut, string invitation)
        {
            sut.Invitation = invitation;
            Assert.That(sut.StartUrl, Is.EqualTo($"{urls.ApprenticeLoginUrl}Invitations/CreatePassword/{invitation}"));
        }
    }
}