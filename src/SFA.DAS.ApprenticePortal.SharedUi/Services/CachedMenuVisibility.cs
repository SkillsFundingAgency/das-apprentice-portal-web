using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public class CachedMenuVisibility : IMenuVisibility
    {
        private readonly Lazy<Task<bool>> _showConfirmMyApprenticeship;
        private readonly Lazy<Task<ConfirmMyApprenticeshipTitleStatus>> _showConfirmMyApprenticeshipTitle;
        private readonly Lazy<Task<bool>> _showApprenticeFeedback;

        public CachedMenuVisibility(IMenuVisibility service)
        {
            _showConfirmMyApprenticeship = new Lazy<Task<bool>>(() => service.ShowConfirmMyApprenticeship());
            _showConfirmMyApprenticeshipTitle = new Lazy<Task<ConfirmMyApprenticeshipTitleStatus>>(() => service.ConfirmMyApprenticeshipTitleStatus());
            _showApprenticeFeedback = new Lazy<Task<bool>>(() => service.ShowApprenticeFeedback());
        }

        public async Task<bool> ShowConfirmMyApprenticeship()
            => await _showConfirmMyApprenticeship.Value;

        public async Task<bool> ShowApprenticeFeedback()
            => await _showApprenticeFeedback.Value;

        public async Task<ConfirmMyApprenticeshipTitleStatus> ConfirmMyApprenticeshipTitleStatus()
            => await _showConfirmMyApprenticeshipTitle.Value;
    }
}