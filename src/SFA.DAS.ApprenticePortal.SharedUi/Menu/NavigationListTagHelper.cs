using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    [HtmlTargetElement(Attributes = "asp-for-external-section")]
    public class NavigationListTagHelper : TagHelper
    {
        private readonly NavigationHelper _helper;

        [HtmlAttributeName("asp-for-external-section")]
        public NavigationSection ExternalSection { get; set; }

        public NavigationListTagHelper(NavigationHelper helper) => _helper = helper;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!await _helper.IsAvailable(ExternalSection))
                output.SuppressOutput();
        }
    }
}