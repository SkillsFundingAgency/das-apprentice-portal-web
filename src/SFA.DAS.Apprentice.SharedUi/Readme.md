# SFA.DAS.Provider.Shared.UI

This package provides shared Apprentice Portal UI components via a [Razor Class Library](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.1&tabs=visual-studio).

This consists of _Layout and _Menu razor views.

## Requirements, Limitations and Restrictions

* Only Core web apps are supported.
* In order for the menu to render, the user must be authenticated

## Usage

* Add a reference to the [SFA.DAS.Apprentice.SharedUI](https://www.nuget.org/packages/SFA.DAS.Apprentice.SharedUI/) package
* Make your base configuration inherit from [ISharedUiConfiguration](https://github.com/SkillsFundingAgency/das-apprentice-portal-web/tree/main/src/SFA.DAS.Apprentice.SharedUi/ISharedUiConfiguration.cs)
* In your configuration add a section called **ApplicationUrls** with DashboardUrl defined.  If desired also add **GoogleAnalytics** and **ZenDesk**.
```
{ 
    "ApplicationUrls": {
        "ApprenticeHomeUrl": "https://localhost:44398",
        "ApprenticeCommitmentsUrl": "https://localhost:7000"
    },
    "GoogleAnalytics": {
        "GoogleTagManagerId": "xxx"
    },
    "ZenDesk": {
        "SectionId": "360000001111",
        "SnippetKey": "CA9E77A1-A25E-4C33-9E05-3E5C26EC65FE",
        "CobrowsingSnippetKey": "zdbckey"
    }
}
```
* In startup.cs add the following, where `appConfig` is an instance of ISharedUiConfiguration with the above config loaded.  Enable Zendesk and Google Analytics if wanted.  Specify the currently selected navigation section.
```
services.AddSharedUi(appConfig, options =>
{
    options.EnableZendesk();            // optional if Zendesk is wanted
    options.EnableGoogleAnalytics();    // optional if GA is wanted
    options.SetCurrentNavigationSection(NavigationSection.Home);
});
```
* Remove any local shared views called \_Layout.cshtml or \_Menu.cshtml, otherwise these will take precedence over those in the package
* Prevent the menu from being displayed by decorating pages with the `HideNavigationBar` attribute filter
