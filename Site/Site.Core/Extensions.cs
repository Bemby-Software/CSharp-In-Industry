using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.DAL;

namespace Site.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IDbFactory, DbFactory>();
            services.AddScoped(provider => provider.GetRequiredService<IDbFactory>().CreateDbConnection());
            return services;
        }
    }
}