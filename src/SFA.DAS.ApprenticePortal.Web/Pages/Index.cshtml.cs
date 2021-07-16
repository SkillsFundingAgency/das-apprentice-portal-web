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
        public string Invitation { get; set; }

        public string StartUrl
        {
            get
            {
                return Invitation == null
                    ? urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, "apprenticeships")
                    : urlHelper.Generate(NavigationSection.Login, $"Invitations/CreatePassword/{Invitation}");
            }
        }

        public IndexModel(NavigationUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }
    }
}