using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeshipService : IApprenticeshipService
    {
        private readonly IOuterApiClient _client;
        private readonly AuthenticatedUser _user;

        public ApprenticeshipService(IOuterApiClient client, AuthenticatedUser user)
            => (_client, _user) = (client, user);

        public async Task<bool> ApprenticeshipExistsForCurrentUser()
            => (await _client.GetApprenticeships(_user.ApprenticeId)).Apprenticeships.Count > 0;
    }
}