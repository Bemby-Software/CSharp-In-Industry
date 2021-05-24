using Microsoft.Extensions.DependencyInjection;
using Site.Core.DAL.Factories;
using Site.Core.DAL.Repositorys;
using Site.Core.DAL.Transactions;

namespace Site.Core.DAL.Exceptions
{
    public static class DALExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IDbFactory, DbFactory>();
            services.AddScoped(provider => provider.GetRequiredService<IDbFactory>().CreateDbConnection());
            services.AddScoped<ISignUpTransaction, SignUpTransaction>();    
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IGitHubAccountRepository, GitHubAccountRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            return services;
        }
    }
}