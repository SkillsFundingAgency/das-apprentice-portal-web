using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeCommitments.Web.Pages
{
    [HideNavigationBar]
    public class IndexModel : PageModel
    {
        public string ApprenticeCommitmentsBaseUrl { get; }

        public IndexModel(NavigationUrlHelper urlHelper)
        {
            ApprenticeCommitmentsBaseUrl = urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, "apprenticeships");
        }
    }
}