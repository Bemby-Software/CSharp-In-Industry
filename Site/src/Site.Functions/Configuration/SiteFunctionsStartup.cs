using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Site.Core;
using Site.Core.Apis;
using Site.Core.Configuration;
using Site.Core.Queues;
using Site.Functions.Configuration;

[assembly:FunctionsStartup(typeof(SiteFunctionsStartup))]

namespace Site.Functions.Configuration
{
    public class SiteFunctionsStartup : FunctionsStartup
    {

        private const string AcceptanceTestingEnvironment = "AcceptanceTesting";
        private const string FunctionsEnvironment = "FUNCTIONS_ENVIRONMENT";
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var settings = ConstructSettings();
            builder.Services
                .AddSingleton(settings)
                .AddCore(settings);
        }

        private ISiteConfiguration ConstructSettings()
        {
            var settings = new SiteConfiguration
            {
                StorageAccountConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
                GithubApiKeys = GetGithubApiKeys().ToArray(),
                MasterRepository = Environment.GetEnvironmentVariable("MasterRepository"),
                GitHubApiUrl = "https://api.github.com/"
            };

            CheckRunningMode(settings);
            
            return settings; 

        }

        private static void CheckRunningMode(SiteConfiguration settings)
        {
            var result = Environment.GetEnvironmentVariable(FunctionsEnvironment);

            if (result == AcceptanceTestingEnvironment)
            {
                settings.GitHubApiUrl = "http://localhost:9000/github/api/";
            }
        }

        private IEnumerable<string> GetGithubApiKeys()
        {
            foreach (DictionaryEntry variable in Environment.GetEnvironmentVariables())
            {
                if (variable is { })
                {
                    if (variable.Key.ToString().StartsWith("GitHubApiKey"))
                        yield return variable.Value.ToString();
                }
            }
        }
    }
}