using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SFA.DAS.ApprenticePortal.Authentication;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class MenuVisibility : IMenuVisibility
    {
        private readonly IOuterApiClient _client;
        private readonly ClaimsPrincipal _user;
        private ApprenticeHomepage? _homePage;

        public MenuVisibility(IOuterApiClient client, IHttpContextAccessor contextAccessor)
        {
            _client = client;
            _user = contextAccessor.HttpContext!.User;
        }

        public async Task<bool> ShowConfirmMyApprenticeship()
        {
            try
            {
                return (await GetHomePageDetails()).Apprenticeship != null;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> ShowApprenticeFeedback() => LatestApprenticeshipIsConfirmed();

        public async Task<ConfirmMyApprenticeshipTitleStatus> ConfirmMyApprenticeshipTitleStatus()
        {
            var showConfirmed = await LatestApprenticeshipIsConfirmed();
            if (showConfirmed)
                return SharedUi.Services.ConfirmMyApprenticeshipTitleStatus.ShowAsConfirmed;
            return SharedUi.Services.ConfirmMyApprenticeshipTitleStatus.ShowAsRequiringConfirmation;
        }

        private async Task<bool> LatestApprenticeshipIsConfirmed()
        {
            try
            {
                var response = await GetHomePageDetails();

                var isConfirmed = response.Apprenticeship?.ConfirmedOn.HasValue ?? false;

                return isConfirmed;
            }
            catch
            {
                return false;
            }
        }

        private async Task<ApprenticeHomepage> GetHomePageDetails()
        {
            if (_homePage != null)
                return _homePage;

            var claim = _user.ApprenticeIdClaim();

            if (!Guid.TryParse(claim?.Value, out var apprenticeId))
                throw new AuthenticationException("No user logged in");

            _homePage = await _client.GetApprenticeHomepage(apprenticeId);
            return _homePage;
        }

        public Task<bool> ShowApprenticeAan()
        {
            return Task.FromResult(true);
        }
    }
}