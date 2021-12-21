using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Web.Services;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.Web.Models;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly ApprenticeService _apprenticesService;
        private readonly AuthenticatedUser _user;

        public HomepageModel? HomePageModel { get; set; }

        public HomeModel(ApprenticeService apprenticesService, AuthenticatedUser user)
        {
            _apprenticesService = apprenticesService;
            _user = user;
        }

        public async Task OnGet()
        {
            try
            {
                HomePageModel = await _apprenticesService.GetHomepageModel(_user.ApprenticeId);
            }
            catch
            {
                // Safely ignore population errors
            }
        }
    }
}
