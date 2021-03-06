using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Respawn;
using Site.Core.Entities;

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
                    catch (Exception)
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

        private static readonly Dictionary<string, string> DatabaseSchema = new();
        
        public static async Task CreateTestDatabase(TestConfiguration settings)
        {
            var connection = new SqlConnection(settings.DbServerConnectionString);
            await connection.OpenAsync();
            var command = connection.CreateCommand();

            try
            {
                command.CommandText = $@"CREATE DATABASE {settings.SqlServerDatabase}";
                command.ExecuteNonQuery();

                if (DatabaseSchema.Count == 0)
                {
                    foreach (var file in Directory.GetFiles(settings.RelativeDatabaseScriptsDirectoryLocation))
                    {
                        if (Path.GetExtension(file) == ".sql")
                        {
                            var contents = await File.ReadAllTextAsync(file);
                            DatabaseSchema.Add(file, contents);
                        }
                    }
                }
                
                foreach (var schema in DatabaseSchema)
                {
                    command.CommandText = $"USE {settings.SqlServerDatabase}; {schema.Value}";
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        private static Checkpoint _checkpoint = new Checkpoint();

        public static async Task RespawnDb()
        {
            await _checkpoint.Reset(TestConfiguration.GetConfiguration().DbConnectionString);
        }

        public static async Task ReCreateDatabase()
        {
            var settings = TestConfiguration.GetConfiguration();
            
            var connection = new SqlConnection(settings.DbServerConnectionString);
            await connection.OpenAsync();
            var command = connection.CreateCommand();

            try
            {
                command.CommandText = $@"
                        USE master;
                        ALTER DATABASE {settings.SqlServerDatabase} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE {settings.SqlServerDatabase} ;
                ";
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    //This code means database does not exist.
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

            await CreateTestDatabase(settings);
        }
    }
}