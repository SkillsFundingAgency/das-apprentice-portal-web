# das-github-template

This repo should be used as a template when creating new repos for the Apprenticeship Service

## Contents

* .github/CODEOWNERS - Defines required approvals for changes to files specified in the CODEOWNERS file
* azure/template.json - Azure ARM template should be used to provision resources on the Azure platform
* .gitignore - Intialised for Visual Studio
* azure-pipelines.yml - Azure Pipelines definition file
* GitVersion.yml - GitVersion configuration file
* LICENSE - License information file
* README.md - Populate with useful information about the repo, the projects it contains and how to get started.
# das-apprentice-portal-web

## Introduction

The das-apprentice-portal-web is one of several microsites which make up Apprentice Commitments.

| Service | Repo |
| ------ | ------ |
| das-apprentice-login-service | https://github.com/SkillsFundingAgency/das-apprentice-login-service |
| das-apprentice-commitments-api | https://github.com/SkillsFundingAgency/das-apprentice-commitments-api |
| das-apim-endpoints | https://github.com/SkillsFundingAgency/das-apim-endpoints |
| das-apprentice-commitments-web | https://github.com/SkillsFundingAgency/das-apprentice-commitments-web |
| das-apprentice-portal-web | https://github.com/SkillsFundingAgency/das-apprentice-portal-web |
| das-apprentice-accounts-web | https://github.com/SkillsFundingAgency/das-apprentice-accounts-web |
| das-apprentice-accounts-api | https://github.com/SkillsFundingAgency/das-apprentice-accounts-api |
| das-apprentice-commitments-jobs | https://github.com/SkillsFundingAgency/das-apprentice-commitments-jobs |

| Configuration | Repo |
| ------ | ------ |
| das-employer-config | https://github.com/SkillsFundingAgency/das-employer-config |
| das-employer-config-updater | https://github.com/SkillsFundingAgency/das-employer-config-updater |


das-apprentice-portal-web solution includes the Apprentice Commitments homepage, SharedUI nuget components which are consumed by other microservicews, as well as an authentication component.

## Developer Setup

The portal uses azure storage to retrieve settings. To use local settings UseDevelopmentStorage=true should be set in the local appsettings.deverlopment.json file as detailed below. Repo das-employer-config section [das-apprentice-portal-web](https://github.com/SkillsFundingAgency/das-employer-config/tree/master/das-apprentice-portal-web) holds the settinge for the portal web.

After cloning the portal repo it should build without any issues.  To run the portal web home page requires a running login service, commitments website & accounts website.  The config section 'ApplicationUrls' indicates where the service should point to (eg with the login service, locally https://localhost:5001 or https://login.at-aas.apprenticeships.education.gov.uk/ for the at environment ).  If you want to run all locally, then the login service, accounts website and commitments website all need to be running locally too. 

### Requirements

### Setup

### Config

The portal webiste hosts the homepage and gets its configuration settings from an appsettings.json file.
Create a local appsettings.development.json file as follows:

    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*",
      "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true",
      "ConfigNames": "SFA.DAS.ApprenticePortal.Web",
      "EnvironmentName": "LOCAL",
      "Cdn": {
        "Url": "https://das-at-frnt-end.azureedge.net"
      },
      "GoogleAnalytics": {
        "GoogleTagManagerId": "GTM-MP5RSWL"
      },
      "ApprenticePortalOuterApi": {
        "ApiBaseUrl": "https://localhost:5123/",
        "SubscriptionKey": "key",
        "ApiVersion": "1"
      },
      "ApplicationUrls": {
        //"ApprenticeHomeUrl": "https://portal.at-aas.apprenticeships.education.gov.uk/",
        //"ApprenticeCommitmentsUrl": "https://commitments.at-aas.apprenticeships.education.gov.uk/",
        //"ApprenticeLoginUrl": "https://login.at-aas.apprenticeships.education.gov.uk/",
        //"ApprenticeLoginUrl": "https://accounts.at-aas.apprenticeships.education.gov.uk/",
        "ApprenticeHomeUrl": "https://localhost:44398",
        "ApprenticeCommitmentsUrl": "https://localhost:7070",
        "ApprenticeLoginUrl": "https://localhost:5001",
        "ApprenticeAccountsUrl": "https://localhost:7080"
      },
      "ZenDesk": {
        "ZenDeskSnippetKey": "e0730bdd-a32c-4c39-8032-7d7a908eacb4",
        "ZenDeskSectionId": "360003002699",
        "ZenDeskCobrowsingSnippetKey": "Qec2OgXsUy8lsUO1iOuleFAMjl7AcIEGdOojlQjLesShBHx0wrM4xXlR7X1h9bEo"
      }
    }