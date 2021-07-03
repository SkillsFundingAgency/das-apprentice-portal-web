using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SFA.DAS.Apprentice.SharedUi.Menu
{
    public static class EnableAttribute
    {
        internal static EnableAttribute<T> With<T>(string key, T data)
            => new EnableAttribute<T>(key, data);
    }

    public class EnableAttribute<T> : IResultFilter
    {
        private readonly string key;
        private readonly T data;

        public EnableAttribute(string key, T data)
        {
            this.data = data;
            this.key = key;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is PageModel page)
                SetViewData(page.ViewData);

            if (context.Controller is Controller controller)
                SetViewData(controller.ViewData);

            void SetViewData(ViewDataDictionary viewData)
                => viewData[key] = data;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}