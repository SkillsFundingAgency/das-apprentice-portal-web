using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Home;
using SFA.DAS.ApprenticePortal.Web.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IOuterApiClient _client;
        private readonly AuthenticatedUser _user;
        private readonly NotificationAccessor _notifications;

        public string CourseName { get; set; } = null!;

        public HomeModel(IOuterApiClient client, AuthenticatedUser user, NotificationAccessor notifications)
        {
            _client = client;
            _user = user;
            _notifications = notifications;
        }

        public async Task OnGet()
        {
            try
            {
                if (!_notifications.Notifications.Contains(HomeNotification.ApprenticeshipDidNotMatch))
                    CourseName = await GetCourseName();
            }
            catch
            {
                _notifications.Notifications.Add(HomeNotification.ApprenticeshipDidNotMatch);
            }
        }

        private async Task<string> GetCourseName()
        {
            var apprenticeships = await _client.GetApprenticeships(_user.ApprenticeId);
            return apprenticeships.Apprenticeships[0].CourseName;
        }
    }
}
