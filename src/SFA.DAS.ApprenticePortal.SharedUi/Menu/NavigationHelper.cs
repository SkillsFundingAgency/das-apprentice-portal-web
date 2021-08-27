using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public class NavigationHelper
    {
        private readonly IApprenticeshipService _apprenticeships;
        private readonly NavigationUrlHelper _helper;

        public NavigationHelper(CachedApprenticeshipService something, NavigationUrlHelper helper)
        {
            _apprenticeships = something;
            _helper = helper;
        }

        public async Task<bool> IsAvailable(NavigationSection externalSection)
            => externalSection switch
            {
                NavigationSection.ConfirmMyApprenticeship => await _apprenticeships.ApprenticeshipExistsForCurrentUser(),
                _ => true,
            };

        internal object Generate(NavigationSection section, string? page)
            => _helper.Generate(section, page);
    }
}