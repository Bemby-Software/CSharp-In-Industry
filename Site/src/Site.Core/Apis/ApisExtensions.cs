using System;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.Apis.GitHub;

namespace Site.Core.Apis
{
    public static class ApisExtensions
    {
        public static IServiceCollection AddGitHubApi(this IServiceCollection services)
        {
            services.AddScoped<IGitHubApi>(_ => new GitHubApi(new WrappedHttpClient(new Uri("https://api.github.com"))));
            return services;
        }   
    }
}