using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Site.Core;
using Site.Core.Configuration;
using Site.Core.DAL.Health;
using Site.Web.Filters;

namespace Site.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string AppPrefix = "comp-site";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new SiteConfiguration();
            _configuration.GetSection($"{AppPrefix}-Settings").Bind(settings);
            services.AddSingleton<ISiteConfiguration>(settings);
            
            
            services.AddCore(settings);
            
            services.AddHealthChecks()
                .AddCheck<DbHealthCheck>("Database");

            services.AddSingleton<IEnvironment, Environment>();
                
            
            services.AddControllers(config => config.Filters.Add(new CoreExceptionFilter()));
            services.AddSpaStaticFiles(config => config.RootPath = "comp-site/dist");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
            
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "comp-site";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
