using RestEase;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Web.Models;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeService
    {
        private readonly IOuterApiClient _client;

        public ApprenticeService(IOuterApiClient client)
        {
            _client = client;
        }

        public async Task<HomepageModel?> GetHomepageModel(Guid apprenticeId)
        {
            try
            {
                var apprenticeHomepage = await _client.GetApprenticeHomepage(apprenticeId);

                if (apprenticeHomepage.Apprentice is null)
                    return null;

                var model = new HomepageModel
                {
                    CourseName = apprenticeHomepage.Apprenticeship.CourseName,
                    EmployerName = apprenticeHomepage.Apprenticeship.EmployerName,
                    Complete = apprenticeHomepage.Apprenticeship.ConfirmedOn.HasValue,
                    HasStopped = apprenticeHomepage.Apprenticeship.IsStopped
                };

                if (apprenticeHomepage.Apprenticeship.IsStopped && (apprenticeHomepage.Apprenticeship.LastViewed == null || apprenticeHomepage.Apprenticeship.LastViewed <= apprenticeHomepage.Apprenticeship.StoppedReceivedOn))
                {
                    model.DisplayJustStoppedInfoMessage = true;
                }

                return model;
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
