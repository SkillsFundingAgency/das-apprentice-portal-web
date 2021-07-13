using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HideNavigationBarAttribute : ResultFilterAttribute
    {
        private bool HideAccountHeader { get; }
        private bool HideNavigationLinks { get; }

        public HideNavigationBarAttribute(bool hideAccountHeader = true, bool hideNavigationLinks = true)
        {
            HideAccountHeader = hideAccountHeader;
            HideNavigationLinks = hideNavigationLinks;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var viewData =
                (context.Controller as Controller)?.ViewData ??
                (context.Controller as PageModel)?.ViewData;

            if (viewData == null) return;

            if (HideNavigationLinks) viewData.HideNavigationMenu();
            if (HideAccountHeader) viewData.HideAccountHeader();
        }
    }
}