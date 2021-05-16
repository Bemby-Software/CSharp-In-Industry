using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton(ConstructSettings())
                .AddCore();
        }

        private ISiteConfiguration ConstructSettings()
        {
            return new SiteConfiguration
            {
                StorageAccountConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
                GithubApiKeys = GetGithubApiKeys().ToArray(),
                MasterRepository = Environment.GetEnvironmentVariable("MasterRepository")
            };
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