using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    public class HelpAndSupportModel : PageModel
    {
        public void OnGet()
        {
            ViewData.SetCurrentNavigationSection(NavigationSection.HelpAndSupport);
        }
    }
}