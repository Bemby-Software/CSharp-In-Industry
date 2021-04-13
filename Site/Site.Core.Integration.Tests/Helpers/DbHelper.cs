using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Site.Core.Integration.Tests.Helpers
{
    public class DbHelper
    {
        public static async Task<IEnumerable<T>> Query<T>(string sql)
        {
            var connection = Startup.TestConnection;
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
            var connection = Startup.TestConnection;
            await connection.OpenAsync();

            try
            {
                return await connection.QueryAsync<T>(sql, param);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        
    }
}