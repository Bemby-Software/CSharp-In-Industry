using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace Site.Testing.Common.Helpers
{
    public static class DbHelper
    {
        
        public static SqlConnection TestConnection => new(
            TestConfiguration.GetConfiguration().DbConnectionString);
        
        public static async Task<IEnumerable<T>> Query<T>(string sql)
        {
            var connection = TestConnection as SqlConnection;
            await connection.OpenAsync();

            try
            {
                return await connection.QueryAsync<T>(sql);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public static async Task<IEnumerable<T>> Query<T>(string sql, object param)
        {
            var connection = TestConnection;
            return await connection.QueryAsync<T>(sql, param);
        }

        public static async Task EnsureStarted(string connectionString, TimeSpan timeout)
        {

            var task = Task.Run(async () =>
            {

                while (true)
                {
                    var connection = new SqlConnection(connectionString);
                    try
                    {

                        await connection.OpenAsync();
                        break;
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            });

            await task.TimeoutAfter(timeout);

        }
        
        public static async Task CreateTestDatabase(TestConfiguration settings)
        {
            var connection = new SqlConnection(settings.DbServerConnectionString);
            await connection.OpenAsync();
            var command = connection.CreateCommand();

            try
            {
                command.CommandText = $@"CREATE DATABASE {settings.SqlServerDatabase}";
                command.ExecuteNonQuery();

                foreach (var file in Directory.GetFiles(settings.RelativeDatabaseScriptsDirectoryLocation))
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
            }
        }
        
        
        
    }
}