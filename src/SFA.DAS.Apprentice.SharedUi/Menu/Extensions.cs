using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace SFA.DAS.Apprentice.SharedUi.Menu
{
    public static class Extensions
    {
        public static IServiceCollection SetWelcomeText(this IServiceCollection services, string welcomeText)
        {
            services.Configure<MvcOptions>(options =>
                options.Filters.Add(EnableAttribute.With(ViewDataKeys.MenuWelcomeText, welcomeText)));
            return services;
        }

        public static string? WelcomeText(this ViewDataDictionary viewData)
            => viewData.TryGetValue(ViewDataKeys.MenuWelcomeText, out var text)
               ? text?.ToString() : null;

        public static string? SelectedNavigationSection(this ViewDataDictionary viewData)
            => (viewData.TryGetValue(ViewDataKeys.SelectedNavigationSection, out var text)
               ? text?.ToString() : null) ?? "Home";
    }
}