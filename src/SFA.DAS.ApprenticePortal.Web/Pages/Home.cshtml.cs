using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Home;
using SFA.DAS.ApprenticePortal.Web.Services;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Web.Models;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly ApprenticeshipService _apprenticeshipService;
        private readonly AuthenticatedUser _user;
        private readonly NotificationAccessor _notifications;

        public ApprenticeshipModel? CurrentApprenticeship { get; set; }

        public ApprenticeshipStatus Status
        {
            get
            {
                if (CurrentApprenticeship == null)
                {
                    return ApprenticeshipStatus.Incomplete;
                }
                if (CurrentApprenticeship.HasStopped)
                {
                    return ApprenticeshipStatus.Stopped;
                }
                if (CurrentApprenticeship.Complete)
                {
                    return ApprenticeshipStatus.Complete;
                }
                return ApprenticeshipStatus.Incomplete;
            }
        }


        public HomeModel(ApprenticeshipService apprenticeshipService, AuthenticatedUser user, NotificationAccessor notifications)
        {
            _apprenticeshipService = apprenticeshipService;
            _user = user;
            _notifications = notifications;
        }

        public async Task OnGet()
        {
            try
            {
                if (!_notifications.Notifications.Contains(HomeNotification.ApprenticeshipDidNotMatch))
                   CurrentApprenticeship = await _apprenticeshipService.GetLatestApprenticeship(_user.ApprenticeId);
            }
            catch
            {
                _notifications.Notifications.Add(HomeNotification.ApprenticeshipDidNotMatch);
            }
        }
    }
}
