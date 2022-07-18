using RestEase;
using SFA.DAS.ApprenticePortal.SharedUi.Home;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeService
    {
        private readonly IOuterApiClient _client;
        private readonly NotificationAccessor _notifications;

        public ApprenticeService(IOuterApiClient client, NotificationAccessor notifications)
        {
            _client = client;
            _notifications = notifications;
        }

        public async Task<HomepageModel?> GetHomepageModel(Guid apprenticeId)
        {
            try
            {
                var apprenticeHomepage = await _client.GetApprenticeHomepage(apprenticeId);

                if (apprenticeHomepage.Apprentice is null)
                    return null;

                var apprenticeship = apprenticeHomepage.Apprenticeship;

                if (apprenticeship?.IsStopped == true &&
                    (apprenticeship.LastViewed == null ||
                     apprenticeship.LastViewed <= apprenticeship.StoppedReceivedOn))
                {
                    _notifications.Notify(HomeNotification.ApprenticeshipStopped);
                }

                var model = new HomepageModel
                {
                    CourseName = apprenticeship?.CourseName,
                    EmployerName = apprenticeship?.EmployerName,
                    Complete = apprenticeship?.ConfirmedOn.HasValue,
                    HasStopped = apprenticeship?.IsStopped,
                    Notification = _notifications.SignificantNotification,
                    Revisions = GetRevisions(apprenticeship?.Revisions),
                };


                return model;
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        private List<Models.Revision> GetRevisions(List<OuterApi.Revision>? revisions)
        { 
            var ret = new List<Models.Revision>();
            if (revisions != null)
                ret.AddRange(revisions.Select(x => new Models.Revision(x.Heading, x.Description, x.RevisionDate)).ToList());

            return ret;
        }
    }
}