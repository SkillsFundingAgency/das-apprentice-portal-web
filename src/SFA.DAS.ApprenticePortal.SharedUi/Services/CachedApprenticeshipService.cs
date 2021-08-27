using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public class CachedApprenticeshipService : IApprenticeshipService
    {
        private readonly IApprenticeshipService _service;
        private bool? cached;

        public CachedApprenticeshipService(IApprenticeshipService service) => _service = service;

        public async Task<bool> ApprenticeshipExistsForCurrentUser()
        {
            if (cached == null) cached = await _service.ApprenticeshipExistsForCurrentUser();
            return cached.Value;
        }
    }
}