using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Docker.DotNet.Models;
using NUnit.Framework;

namespace Site.Core.Integration.Tests.Helpers
{
    public class DbHelper
    {
        public static async Task<IEnumerable<T>> Query<T>(string sql)
        {
            var connection = TestStartup.TestConnection;
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
            var connection = TestStartup.TestConnection;
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
        
        
        
    }
}