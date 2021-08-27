using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public interface IApprenticeshipService
    {
        Task<bool> ApprenticeshipExistsForCurrentUser();
    }
}