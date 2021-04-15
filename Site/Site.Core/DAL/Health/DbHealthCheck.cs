using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Site.Core.DAL.Health
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<DbHealthCheck> _logger;

        public DbHealthCheck(IDbConnection dbConnection, ILogger<DbHealthCheck> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                _dbConnection.Open();
                await _dbConnection.ExecuteAsync("SELECT 1");
            }
            catch (Exception e)
            {
                _logger.LogError($"Health check failed on database with error: {e}");
                return HealthCheckResult.Healthy("Database connection attempt failed.");
            }
            finally
            {
                _dbConnection.Close();
            }
            
            return HealthCheckResult.Healthy();
        }
    }
}