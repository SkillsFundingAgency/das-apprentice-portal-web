using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Authentication;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class MenuVisibility : IMenuVisibility
    {
        private readonly IOuterApiClient _client;
        private readonly ClaimsPrincipal _user;
        private ApprenticeHomepage? _homePage;

        public MenuVisibility(IOuterApiClient client, ClaimsPrincipal user)
        {
            _client = client;
            _user = user;
        }

        public async Task<bool> ShowConfirmMyApprenticeship()
        {
            var claim = _user.ApprenticeIdClaim();

            if (!Guid.TryParse(claim?.Value, out var apprenticeId))
                return false;

            try
            {
                return (await GetHomePage(apprenticeId)).Apprenticeship != null;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> ShowApprenticeFeedback() => LatestApprenticeshipIsConfirmed();

        public async Task<bool> ShowConfirmOnMyApprenticeshipTitle()
        {
            return !await LatestApprenticeshipIsConfirmed();
        }

        private async Task<bool> LatestApprenticeshipIsConfirmed()
        {
            var claim = _user.ApprenticeIdClaim();

            if (!Guid.TryParse(claim?.Value, out var apprenticeId))
                return false;

            try
            {
                var response = await GetHomePage(apprenticeId);

                var isConfirmed = response.Apprenticeship?.ConfirmedOn.HasValue ?? false;

                return isConfirmed;
            }
            catch
            {
                return false;
            }
        }

        private async Task<ApprenticeHomepage> GetHomePage(Guid id)
        {
            if (_homePage != null)
                return _homePage;

            _homePage = await _client.GetApprenticeHomepage(id);
            return _homePage;
        }
    }
}