using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.SharedUi.Identity;
using SFA.DAS.ApprenticePortal.Web.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        //private readonly IOuterApiClient _client;
        //private readonly AuthenticatedUser _authenticatedUser;

        //[BindProperty(SupportsGet = true)]
        //public HashedId ApprenticeshipId { get; set; }

        public string ApprenticeCommitmentsBaseUrl { get; set; }

        //public string CourseName { get; set; } = null!;

        //public HomeModel(IOuterApiClient client, AuthenticatedUser authenticatedUser)
        //{
        //    _client = client;
        //    _authenticatedUser = authenticatedUser;
        //}

        //public async Task OnGet()
        //{
        //    var apprenticeship = await _client
        //        .GetApprenticeship(_authenticatedUser.ApprenticeId, ApprenticeshipId.Id);

        //    CourseName = apprenticeship.CourseName;
        //}
    }
}
