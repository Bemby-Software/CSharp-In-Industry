using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Site.Core.DAL.Exceptions;

namespace Site.Core.DAL
{
    // ReSharper disable once InconsistentNaming
    public static class DALExtensions
    {
        public static Task<IEnumerable<T>> ExecuteQueryAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null)
        {
            try
            {
                connection.Open();

                return connection.QueryAsync<T>(sql, parameter, transaction);
            }
            catch (Exception e)
            {
                throw new DataAccessException(e);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}