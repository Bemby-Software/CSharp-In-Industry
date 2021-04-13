using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Site.Core.Integration.Tests.Helpers;

namespace Site.Core.Integration.Tests
{
    [SetUpFixture]
    public class Startup
    {
        
        

        public static SqlConnection TestConnection =>
            new SqlConnection("Server=localhost;Database=SiteTestDb;User Id=SA;Password=P@ssword123;");
        
        [OneTimeSetUp]
        public async Task SetupDatabaseAsync()
        {

            await SqlServerContainer.StartAsync();


            // var connection = new SqlConnection(@"Server=localhost;User Id=SA;Password=P@ssword123;");
            // await connection.OpenAsync();
            // var command = connection.CreateCommand();
            //
            // try
            // {
            //     command.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SiteTestDb')
            //                                 BEGIN
            //                                     CREATE DATABASE SiteTestDb;
            //                                 END;
            //                                 ";
            //     command.ExecuteNonQuery();
            //
            //     foreach (var file in Directory.GetFiles("db"))
            //     {
            //         if (Path.GetExtension(file) == ".sql")
            //         {
            //             var contents = await File.ReadAllTextAsync(file);
            //             command.CommandText = $"USE SiteTestDb; {contents}";
            //             command.ExecuteNonQuery();
            //         }
            //     }
            //     
            // }
            // finally
            // {
            //     await connection.CloseAsync();
            // }            
        }

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