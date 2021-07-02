using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.Apprentice.SharedUi
{
    [HtmlTargetElement("a", Attributes = "asp-external-page")]
    public class ExtendedAnchorTagHelper : AnchorTagHelper
    {
        private readonly ExternalUrlHelper _helper;

        [HtmlAttributeName("asp-external-page")]
        public string? ExternalPage { get; set; }

        [HtmlAttributeName("asp-external-subdomain")]
        public string? ExternalSubDomain { get; set; }

        public ExtendedAnchorTagHelper(IHtmlGenerator generator, ExternalUrlHelper helper)
            : base(generator)
        {
            _helper = helper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            var href = _helper.Generate(controller: ExternalPage);
            output.Attributes.SetAttribute("href", href);
        }
    }
}