using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Apprentice.SharedUi.Menu;

namespace SFA.DAS.Apprentice.SharedUi
{
    [HtmlTargetElement("a", Attributes = "asp-external-section")]
    public class NavigationAnchorTagHelper : AnchorTagHelper
    {
        private readonly NavigationUrlHelper _helper;

        [HtmlAttributeName("asp-external-section")]
        public NavigationSection ExternalSection { get; set; }

        [HtmlAttributeName("asp-external-page")]
        public string? ExternalPage { get; set; }

        [HtmlAttributeName("asp-external-subdomain")]
        public string? ExternalSubDomain { get; set; }

        public NavigationAnchorTagHelper(IHtmlGenerator generator, NavigationUrlHelper helper)
            : base(generator)
        {
            _helper = helper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            var href = _helper.Generate(section: ExternalSection, page: ExternalPage);
            output.Attributes.SetAttribute("href", href);
        }
    }
}