using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.DAL;
using Site.Core.DAL.Factories;
using Site.Core.DAL.Repositorys;
using Site.Core.DAL.Transactions;
using Site.Core.Factories;
using Site.Core.Helpers;
using Site.Core.Services;

namespace Site.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IDbFactory, DbFactory>();
            services.AddScoped(provider => provider.GetRequiredService<IDbFactory>().CreateDbConnection());
            services.AddScoped<ISignUpTransaction, SignUpTransaction>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            return services;
        }

        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);
    }
}