using SFA.DAS.Apprentice.SharedUi.Menu;

namespace SFA.DAS.Apprentice.SharedUi
{
    public class NavigationUrlHelper
    {
        private readonly NavigationSectionUrls urls;

        public NavigationUrlHelper(NavigationSectionUrls urls) => this.urls = urls;

        public string Generate(NavigationSection section, string? page = null)
        {
            var sectionUrl = urls.ToUri(section);
            return new ExternalUrlHelper(sectionUrl).Generate(page);
        }
    }
}