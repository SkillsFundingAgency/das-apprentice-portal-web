using RestEase;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services.OuterApi
{
    public interface IOuterApiClient
    {
        [Get("/apprentices/{id}/apprenticeships")]
        Task<Apprenticeship[]> GetApprenticeships([Path] Guid id);

        [Get("/apprentices/{apprenticeid}/apprenticeships/{apprenticeshipid}")]
        Task<Apprenticeship> GetApprenticeship([Path] Guid apprenticeid, [Path] long apprenticeshipid);
    }
}
