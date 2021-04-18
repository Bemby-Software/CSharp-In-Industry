using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Docker.DotNet.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Site.Core.Integration.Tests.Helpers;
using Timer = System.Threading.Timer;

namespace Site.Core.Integration.Tests
{
    [SetUpFixture]
    public class TestStartup
    {



        public static SqlConnection TestConnection =>
            new SqlConnection(TestConfiguration.GetConfiguration().DbConnectionString);
            

        [OneTimeSetUp]
        public async Task SetupDatabaseAsync()
        {
            
            await SqlServerContainer.StartAsync();
            var settings = TestConfiguration.GetConfiguration();

            await DbHelper.EnsureStarted(settings.DbServerConnectionString, TimeSpan.FromSeconds(60));


            var connection = new SqlConnection(settings.DbServerConnectionString);
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            
            try
            {
                command.CommandText = $@"CREATE DATABASE {settings.SqlServerDatabase}";
                command.ExecuteNonQuery();
            
                foreach (var file in Directory.GetFiles("db"))
                {
                    if (Path.GetExtension(file) == ".sql")
                    {
                        var contents = await File.ReadAllTextAsync(file);
                        command.CommandText = $"USE {settings.SqlServerDatabase}; {contents}";
                        command.ExecuteNonQuery();
                    }
                }
                
            }
            finally
            {
                await connection.CloseAsync();
            }                   }

        public static IDbConnection GetTestConnection() => new SqlConnection(@"Server=localhost;Database=SiteTestDb;User Id=SA;Password=P@ssword123;");

        [OneTimeTearDown]
        public async Task RemoveDatabaseAsync()
        {
            var connection = new SqlConnection("Server=localhost;User Id=SA;Password=P@ssword123;");
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            
            try
            {
                command.CommandText = @"DROP DATABASE SiteTestDb";
                command.ExecuteNonQuery();
            
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}