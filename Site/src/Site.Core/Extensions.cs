using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.Apis;
using Site.Core.Apis.GitHub;
using Site.Core.Configuration;
using Site.Core.DAL;
using Site.Core.DAL.Exceptions;
using Site.Core.DAL.Factories;
using Site.Core.DAL.Repositorys;
using Site.Core.DAL.Transactions;
using Site.Core.Factories;
using Site.Core.Helpers;
using Site.Core.Queues;
using Site.Core.Services;

namespace Site.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddDataAccessLayer();
            services.AddQueueServices();
            services.AddGitHubApi();
            services.AddScoped<IGitHubAccountService, GitHubAccountService>();
            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            return services;
        }

        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsNullOrEmpty<T>(this List<T> enumerable) => enumerable is null || !enumerable.Any();

        public static IEnumerable<TResult> SelectOrEmpty<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector) 
            => enumerable?.Select(selector) ?? new List<TResult>();

        public static DateTime ToSecondsTolerance(this DateTime dt) =>
            new(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
    }
}