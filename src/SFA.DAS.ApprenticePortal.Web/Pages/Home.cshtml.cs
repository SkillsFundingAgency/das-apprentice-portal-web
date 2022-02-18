using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Web.Services;
using System.Threading.Tasks;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Web.Models;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    [RequiresIdentityConfirmed]
    public class HomeModel : PageModel
    {
        private readonly ApprenticeService _apprenticesService;
        private readonly AuthenticatedUser _user;
        private readonly NavigationUrlHelper _urlHelper;

        public HomepageModel? HomePageModel { get; set; }

        public HomeModel(ApprenticeService apprenticesService, AuthenticatedUser user, NavigationUrlHelper urlHelper)
        {
            _apprenticesService = apprenticesService;
            _user = user;
            _urlHelper = urlHelper;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (Request.Cookies.TryGetValue("RegistrationCode", out var registrationCode))
                    return Redirect(_urlHelper.Generate(NavigationSection.Registration));
                else
                {
                    HomePageModel = await _apprenticesService.GetHomepageModel(_user.ApprenticeId);

                    if (HomePageModel?.CourseName == null && HomePageModel?.EmployerName == null)
                        return Redirect(_urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship));
                }
            }
            catch
            {
                // Safely ignore population errors                
            }

            return new PageResult();
        }
    }
}
