using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.ApprenticePortal.SharedUi.Services;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu;

[HtmlTargetElement(Attributes = "asp-confirm-apprenticeship-menu-name")]
public class ConfirmMyApprenticeshipMenuTitleTagHelper : TagHelper
{
    private readonly CachedMenuVisibility _helper;

    public ConfirmMyApprenticeshipMenuTitleTagHelper(CachedMenuVisibility helper) => _helper = helper;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var menuTitle = "My apprenticeship details";
        if (await _helper.ShowConfirmOnMyApprenticeshipTitle())
        {
            menuTitle = "Confirm my apprenticeship details";

        }
        output.Content.SetContent(menuTitle);
    }
}