using RestEase;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services.OuterApi
{
    public interface IOuterApiClient
    {
        [Get("/apprentices/{id}/apprenticeships")]
        Task<ApprenticeshipsWrapper> GetApprenticeships([Path] Guid id);
    }
}