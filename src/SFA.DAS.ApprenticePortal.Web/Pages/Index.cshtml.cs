using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Web.Startup;

namespace SFA.DAS.ApprenticeCommitments.Web.Pages
{
    [HideNavigationBar]
    public class IndexModel : PageModel
    {
        private readonly NavigationUrlHelper urlHelper;
        private readonly ApplicationConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public string? Invitation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Register { get; set; }

        public string StartUrl
        {
            get
            {
                if (_configuration.UseGovSignIn)
                {
                    if(Invitation != null || Register != null)
                        return urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, $"register/{Register}");
                }
                else
                {
                    if (Invitation != null)
                        return urlHelper.Generate(NavigationSection.Login, $"Invitations/CreatePassword/{Invitation}");
                    else if (Register != null)
                        return urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, $"register/{Register}");
                }
                return urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, "apprenticeships");
            }
        }

        public IndexModel(NavigationUrlHelper urlHelper, ApplicationConfiguration configuration)
        {
            this.urlHelper = urlHelper;
            _configuration = configuration;
        }
    }
}