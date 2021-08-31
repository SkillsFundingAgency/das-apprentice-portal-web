using SFA.DAS.ApprenticePortal.SharedUi.Identity;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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

            return (await _client.GetApprenticeships(apprenticeId)).Apprenticeships.Count > 0;
        }
    }
}