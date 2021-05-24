using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Core.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Site.Functions
{
    public class Health
    {
        private readonly ISiteConfiguration _siteConfiguration;

        public Health(ISiteConfiguration siteConfiguration)
        {
            _siteConfiguration = siteConfiguration;
        }
        
        
        
        
        [FunctionName("Health")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Functions are alive");
            
            log.LogInformation($"Settings: {JsonSerializer.Serialize(_siteConfiguration, new JsonSerializerOptions(){WriteIndented = true})}");

            return new OkResult();

        }
    }
}