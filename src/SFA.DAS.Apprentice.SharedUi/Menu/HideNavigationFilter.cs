using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SFA.DAS.Apprentice.SharedUi.Menu
{
    public interface IHideNavigation
    {
    }

    public class HidenNavigationResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is IHideNavigation)
            {
                (context.Controller as PageModel)?.ViewData.HideNavigationMenu();
                (context.Controller as Controller)?.ViewData.HideNavigationMenu();
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}