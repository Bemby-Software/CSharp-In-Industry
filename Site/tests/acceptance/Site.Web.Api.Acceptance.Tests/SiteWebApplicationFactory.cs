using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.Configuration;
using Site.Testing.Common.Helpers;

namespace Site.Web.Acceptance
{
    public class SiteWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var config = new SiteConfiguration()
                {
                    DbConnectionString = TestConfiguration.GetConfiguration().DbConnectionString
                };

                services.AddSingleton<ISiteConfiguration>(config);
            });
        }
    }
}