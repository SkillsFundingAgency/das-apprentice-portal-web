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

        public MenuVisibility(IOuterApiClient client, ClaimsPrincipal user)
            => (_client, _user) = (client, user);

        public async Task<bool> ShowConfirmMyApprenticeship()
        {
            var claim = _user.ApprenticeIdClaim();

            if (!Guid.TryParse(claim?.Value, out var apprenticeId))
                return false;

            try
            {
                return (await _client.GetApprenticeHomepage(apprenticeId)).Apprenticeship != null;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> ShowApprenticeFeedback()
        {
            var claim = _user.ApprenticeIdClaim();

            if (!Guid.TryParse(claim?.Value, out var apprenticeId))
                return false;

            try
            {
                var response = await _client.GetApprenticeHomepage(apprenticeId);

                var isStopped = response.Apprenticeship?.IsStopped == true;
                var isConfirmed = response.Apprenticeship?.ConfirmedOn.HasValue ?? false;
                var isPrivateBeta = response.Apprentice?.IsPrivateBetaUser == true;

                return isPrivateBeta && isConfirmed && !isStopped;
            }
            catch
            {
                return false;
            }
        }
    }
}