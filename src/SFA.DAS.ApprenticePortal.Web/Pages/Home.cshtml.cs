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
        private readonly ApprenticeshipService _apprenticeshipService;
        private readonly AuthenticatedUser _user;

        public ApprenticeshipModel? CurrentApprenticeship { get; set; }

        public HomeModel(ApprenticeshipService apprenticeshipService, AuthenticatedUser user)
        {
            _apprenticeshipService = apprenticeshipService;
            _user = user;
        }

        public async Task OnGet()
        {
            try
            {
                CurrentApprenticeship = await _apprenticeshipService.GetLatestApprenticeship(_user.ApprenticeId);
            }
            catch
            {
                // Safely ignore population errors
            }
        }
    }
}
