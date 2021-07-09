using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SFA.DAS.ApprenticePortal.Web.Views
{
    public class HomepageModel : PageModel
    {
        //[BindProperty]
        //public string CourseName { get; set; } = null!;
        public string CourseName = "My Apprenticeship";
        public string ApprenticeCommitmentsBaseUrl { get; set; }

        public void OnGet()
        {
            //CourseName = apprenticeship.CourseName;
        }
    }
}
