using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeshipService : IApprenticeshipService
    {
        public Task<bool> ApprenticeshipExistsForCurrentUser()
        {
            return Task.FromResult(false);
        }
    }
}