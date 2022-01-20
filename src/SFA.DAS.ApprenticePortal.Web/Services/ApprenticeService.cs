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

                var apprenticeship = apprenticeHomepage.Apprenticeship;

                var model = new HomepageModel
                {
                    CourseName = apprenticeship?.CourseName,
                    EmployerName = apprenticeship?.EmployerName,
                    Complete = apprenticeship?.ConfirmedOn.HasValue,
                    HasStopped = apprenticeship?.IsStopped
                };

                if (apprenticeship != null && apprenticeship.IsStopped && (apprenticeship.LastViewed == null || apprenticeship.LastViewed <= apprenticeship.StoppedReceivedOn))
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
