using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi
{
    public interface IApprenticeshipService
    {
        Task<bool> ApprenticeshipExistsForCurrentUser();
    }
}