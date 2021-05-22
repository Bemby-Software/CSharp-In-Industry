using System;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.Apis.GitHub;

namespace Site.Core.Apis
{
    public static class ApisExtensions
    {
        public static IServiceCollection AddGitHubApi(this IServiceCollection services, string apiUrl)
        {
            services.AddScoped<IGitHubApi>(_ => new GitHubApi(new WrappedHttpClient(new Uri(apiUrl))));
            return services;
        }   
    }
}