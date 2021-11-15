using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Web.Services;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IOuterApiClient _client;
        private readonly AuthenticatedUser _user;

        public string CourseName { get; set; } = null!;
        public bool ApprenticeshipComplete { get; set; } = false;

        public HomeModel(IOuterApiClient client, AuthenticatedUser user)
        {
            _client = client;
            _user = user;
        }

        public async Task OnGet()
        {
            try
            {
                await PopulateProperties();
            }
            catch
            {
            }
        }

        private async Task PopulateProperties()
        {
            var apprenticeships = await _client.GetApprenticeships(_user.ApprenticeId);
            CourseName = apprenticeships.Apprenticeships[0].CourseName;
            ApprenticeshipComplete = apprenticeships.Apprenticeships[0].ConfirmedOn.HasValue;
        }
    }
}
