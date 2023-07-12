﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;

namespace SFA.DAS.ApprenticePortal.Web.Startup
{
    public static class ConfigurationStartup
    {
        public static IWebHostBuilder ConfigureAzureTableConfiguration(this IWebHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
            {
                var config = configBuilder.Build();

                if (!config["EnvironmentName"].Equals("DEV", StringComparison.InvariantCultureIgnoreCase))
                {
                    configBuilder.AddAzureTableStorage(options =>
                    {
                        var (names, connectionString, environment) = GetEmployerConfiguration(config);
                        options.ConfigurationKeys = names.Split(",");
                        options.StorageConnectionString = connectionString;
                        options.EnvironmentName = environment;
                        options.PreFixConfigurationKeys = false;
                    });
                }
                configBuilder.AddJsonFile("appsettings.development.json", true);
            });

            return hostBuilder;
        }

        private static (string names, string connectionString, string environment) GetEmployerConfiguration(IConfigurationRoot config)
            => (config["ConfigNames"], config["ConfigurationStorageConnectionString"], config["EnvironmentName"]);
    }
}