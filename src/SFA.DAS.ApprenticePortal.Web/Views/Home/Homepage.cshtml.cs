using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SFA.DAS.ApprenticePortal.Web.Views
{
    public class HomepageModel : PageModel
    {
        public string ApprenticeCommitmentsBaseUrl { get; set; }

        public void OnGet()
        {
        }
    }
}
