using RestEase;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeService
    {
        private readonly IOuterApiClient _client;

        public ApprenticeService(IOuterApiClient client)
        {
            _client = client;
        }

        public async Task<Apprentice?> TryGetApprentice(Guid apprenticeId)
        {
            try
            {
                return await _client.GetApprentice(apprenticeId);
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
