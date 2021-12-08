using System;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeshipService
    {
        private readonly IOuterApiClient _client;

        public ApprenticeshipService(IOuterApiClient client)
        {
            _client = client;
        }

        public async Task<ApprenticeshipModel?> GetLatestApprenticeship(Guid apprenticeId)
        {
            var latest = (await _client.GetApprenticeships(apprenticeId)).Apprenticeships.FirstOrDefault();

            if (latest == null)
                return null;

            var model = new ApprenticeshipModel
            {
                CourseName = latest.CourseName,
                EmployerName = latest.EmployerName,
                Complete = latest.ConfirmedOn.HasValue,
                HasStopped = latest.IsStopped
            };

            if (latest.IsStopped && (latest.LastViewed == null || latest.LastViewed <= latest.StoppedReceivedOn))
            {
                model.DisplayJustStoppedInfoMessage = true;
            }

            return model;
        }
    }
}