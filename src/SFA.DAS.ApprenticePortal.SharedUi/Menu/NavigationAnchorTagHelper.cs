using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    [HtmlTargetElement("a", Attributes = "asp-external-section")]
    public class NavigationAnchorTagHelper : TagHelper
    {
        private readonly NavigationHelper _helper;

        [HtmlAttributeName("asp-external-section")]
        public NavigationSection ExternalSection { get; set; }

        [HtmlAttributeName("asp-external-page")]
        public string? ExternalPage { get; set; }

        [HtmlAttributeName("asp-always-show")]
        public bool AlwaysShow { get; set; }

        public NavigationAnchorTagHelper(NavigationHelper helper) => _helper = helper;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (await ShouldShow())
            {
                var href = _helper.Generate(section: ExternalSection, page: ExternalPage);
                output.Attributes.SetAttribute("href", href);
            }
            else
            {
                output.SuppressOutput();
            }
        }

        private async Task<bool> ShouldShow()
            => AlwaysShow || await _helper.IsAvailable(ExternalSection);
    }
}