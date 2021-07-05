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

        public static void HideNavigationMenu(this ViewDataDictionary viewData)
        {
            viewData[ViewDataKeys.HideNavigationLinks] = true;
        }

        public static bool IsNavigationMenuVisible(this ViewDataDictionary viewData) =>
            viewData.TryGetValue(ViewDataKeys.HideNavigationLinks, out var text)
                ? (text as bool?) != true
                : true;

        public static IMvcBuilder SetCurrentNavigationSection(this IMvcBuilder builder, NavigationSection defaultSection)
        {
            builder.Services.Configure<MvcOptions>(options =>
                options.Filters.Add(EnableAttribute.With(ViewDataKeys.CurrentNavigationSection, defaultSection)));
            return builder;
        }

        public static void SetCurrentNavigationSection(this ViewDataDictionary viewData, NavigationSection defaultSection)
        {
            viewData[ViewDataKeys.CurrentNavigationSection] = defaultSection;
        }

        public static NavigationSection? SelectedNavigationSection(this ViewDataDictionary viewData)
            => viewData.TryGetValue(ViewDataKeys.CurrentNavigationSection, out var text)
               ? text as NavigationSection? : null;
    }
}