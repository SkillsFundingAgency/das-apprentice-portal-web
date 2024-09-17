using RestEase;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services.OuterApi
{
    public interface IOuterApiClient
    {
        [Get("/apprentices/{id}")]
        Task<Apprentice> GetApprentice([Path] Guid id);

        [Get("/apprentices/{id}/homepage")]
        Task<ApprenticeHomepage> GetApprenticeHomepage([Path] Guid id);

        [Put("/apprentices")]
        Task<Apprentice> PutApprentice([Body] PutApprenticeRequest request);
    }
}