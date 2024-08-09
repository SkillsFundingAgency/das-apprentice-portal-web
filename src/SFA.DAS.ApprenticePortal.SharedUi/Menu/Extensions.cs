using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace SFA.DAS.ApprenticePortal.SharedUi.Menu
{
    public static class Extensions
    {
        public static IServiceCollection SetWelcomeText(this IServiceCollection services, string welcomeText)
        {
            services.Configure<MvcOptions>(options =>
                options.Filters.Add(EnableAttribute.With(ViewDataKeys.MenuWelcomeText, welcomeText)));
            return services;
        }

        public static void SetWelcomeText(this ViewDataDictionary viewData, string welcomeText)
        {
            viewData[ViewDataKeys.MenuWelcomeText] = welcomeText;
        }

        public static string? WelcomeText(this ViewDataDictionary viewData)
            => viewData.TryGetValue(ViewDataKeys.MenuWelcomeText, out var text)
               ? text?.ToString() : null;

        public static void HideNavigationMenu(this ViewDataDictionary viewData)
        {
            viewData[ViewDataKeys.HideNavigationLinks] = true;
        }

        public static void HideAccountHeader(this ViewDataDictionary viewData)
        {
            viewData[ViewDataKeys.HideAccountHeader] = true;
        }

        public static bool IsNavigationMenuVisible(this ViewDataDictionary viewData) =>
            (viewData[ViewDataKeys.HideNavigationLinks] as bool?) != true;

        public static IServiceCollection SetCurrentNavigationSection(this IServiceCollection services, NavigationSection defaultSection)
        {
            services.Configure<MvcOptions>(options =>
                options.Filters.Add(EnableAttribute.With(ViewDataKeys.CurrentNavigationSection, defaultSection)));
            return services;
        }

        public static void SetCurrentNavigationSection(this ViewDataDictionary viewData, NavigationSection defaultSection)
        {
            viewData[ViewDataKeys.CurrentNavigationSection] = defaultSection;
        }

        public static NavigationSection? SelectedNavigationSection(this ViewDataDictionary viewData)
            => viewData.TryGetValue(ViewDataKeys.CurrentNavigationSection, out var text)
               ? text as NavigationSection? : null;

        public static IServiceCollection UseGovSign(this IServiceCollection services, bool useGovSignIn)
        {
            services.Configure<MvcOptions>(options =>
                options.Filters.Add(EnableAttribute.With(ViewDataKeys.UseGovSignIn, useGovSignIn)));
            return services;
        }

        public static bool UseGovSignIn(this ViewDataDictionary viewData)
        {
            if (viewData.TryGetValue(ViewDataKeys.UseGovSignIn, out var text))
            {
                if (bool.TryParse(text?.ToString(), out bool parsedValue))
                {
                    return parsedValue;
                }
            }

            return false;
        }
    }
}