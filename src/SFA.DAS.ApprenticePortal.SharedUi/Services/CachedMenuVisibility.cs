using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public class CachedMenuVisibility : IMenuVisibility
    {
        private readonly Lazy<Task<bool>> _visible;

        public CachedMenuVisibility(IMenuVisibility service)
            => _visible = new Lazy<Task<bool>>(() => service.ShowConfirmMyApprenticeship());

        public async Task<bool> ShowConfirmMyApprenticeship()
            => await _visible.Value;
    }
}