using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeCommitments.Web.Pages
{
    [HideNavigationBar]
    public class IndexModel : PageModel
    {
        private readonly NavigationUrlHelper urlHelper;

        [BindProperty(SupportsGet = true)]
        public string? Invitation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Register { get; set; }

        public string StartUrl
        {
            get
            {
                if (Invitation != null)
                    return urlHelper.Generate(NavigationSection.Login, $"Invitations/CreatePassword/{Invitation}");
                else if (Register != null)
                    return urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, $"register/{Register}");
                else
                    return urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, "apprenticeships");
            }
        }

        public IndexModel(NavigationUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }
    }
}