using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public class NavigationHelper
    {
        private readonly IMenuVisibility _menu;
        private readonly NavigationUrlHelper _helper;

        public NavigationHelper(CachedMenuVisibility something, NavigationUrlHelper helper)
        {
            _menu = something;
            _helper = helper;
        }

        public async Task<bool> IsAvailable(NavigationSection externalSection)
            => externalSection switch
            {
                NavigationSection.ConfirmMyApprenticeship => await _menu.ShowConfirmMyApprenticeship(),
                _ => true,
            };

        internal object Generate(NavigationSection section, string? page)
            => _helper.Generate(section, page);
    }
}