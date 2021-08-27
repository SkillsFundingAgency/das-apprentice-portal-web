using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public class CachedApprenticeshipService : IMenuVisibility
    {
        private readonly IMenuVisibility _service;
        private bool? cached;

        public CachedApprenticeshipService(IMenuVisibility service) => _service = service;

        public async Task<bool> ShowConfirmMyApprenticeship()
        {
            if (cached == null) cached = await _service.ShowConfirmMyApprenticeship();
            return cached.Value;
        }
    }
}