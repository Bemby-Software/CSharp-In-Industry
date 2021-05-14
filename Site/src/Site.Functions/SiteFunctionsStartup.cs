
using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Site.Core.Configuration;
using Site.Core.Queues;
using Site.Functions;

[assembly:FunctionsStartup(typeof(SiteFunctionsStartup))]

namespace Site.Functions
{
    public class SiteFunctionsStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
           
            builder.Services.AddSingleton(ConstructSettings());
            builder.Services.AddQueueServices();
        }

        private ISiteConfiguration ConstructSettings()
        {
            return new SiteConfiguration
            {
                StorageAccountConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage")
            };
        }
    }
}