using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SFA.DAS.ApprenticePortal.Web.Views
{
    public class HomeModel : PageModel
    {
        //[BindProperty]
        //public string CourseName { get; set; } = null!;
        public string CourseName = "My Apprenticeship";
        public string ApprenticeCommitmentsBaseUrl { get; set; }

        public HomeModel()
        {
            //_client = client;
            //_authenticatedUser = authenticatedUser;
            
        }

        public void OnGet(/*IOuterApiClient client, AuthenticatedUser authenticatedUser*/)
        {
            //var apprenticeship = await _client
            //    .GetApprenticeship(_authenticatedUser.ApprenticeId, ApprenticeshipId.Id);

            //CourseName = apprenticeship.CourseName;
        }
    }
}
