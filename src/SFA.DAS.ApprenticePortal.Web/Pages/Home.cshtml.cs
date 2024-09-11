using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Web.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Startup;
using SFA.DAS.GovUK.Auth.Services;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    [RequiresIdentityConfirmed]
    public class HomeModel : PageModel
    {
        private readonly ApprenticeService _apprenticesService;
        private readonly AuthenticatedUser _user;
        private readonly NavigationUrlHelper _urlHelper;
        private readonly ApplicationConfiguration _configuration;
        private readonly IOidcService _oidcService;
        private readonly ApprenticeAccountProvider _apprenticeAccountProvider;

        public HomepageModel? HomePageModel { get; set; }

        public HomeModel(ApprenticeService apprenticesService, AuthenticatedUser user, NavigationUrlHelper urlHelper, ApplicationConfiguration configuration, IOidcService oidcService, ApprenticeAccountProvider apprenticeAccountProvider)
        {
            _apprenticesService = apprenticesService;
            _user = user;
            _urlHelper = urlHelper;
            _configuration = configuration;
            _oidcService = oidcService;
            _apprenticeAccountProvider = apprenticeAccountProvider;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (_configuration.UseGovSignIn && !_configuration.UseStubAuth)
                {
                    var token = await HttpContext.GetTokenAsync("access_token");
                    var govUkUser = await _oidcService.GetAccountDetails(token);
                    if (!govUkUser.Email.Equals(_user.Email!.Address, StringComparison.CurrentCultureIgnoreCase))
                    {
                        await _apprenticeAccountProvider.PutApprenticeAccount(govUkUser.Email, govUkUser.Sub);
                    }
                }
                
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
