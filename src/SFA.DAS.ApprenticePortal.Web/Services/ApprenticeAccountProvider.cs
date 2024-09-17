using System;
using System.Threading.Tasks;
using RestEase;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeAccountProvider : IApprenticeAccountProvider
    {
        private readonly IOuterApiClient _client;

        public ApprenticeAccountProvider(IOuterApiClient client)
        {
            _client = client;
        }
        public async Task<IApprenticeAccount?> GetApprenticeAccount(Guid id)
        {
            try
            {
                return await _client.GetApprentice(id);
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IApprenticeAccount?> PutApprenticeAccount(string email, string govIdentifier)
        {
            return await _client.PutApprentice(new PutApprenticeRequest(email, govIdentifier));
        }
    }
}