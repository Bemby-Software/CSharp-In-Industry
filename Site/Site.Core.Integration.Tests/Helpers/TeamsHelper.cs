using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Site.Core.Entities;
using Site.Core.Integration.Tests.Models;

namespace Site.Core.Integration.Tests.Helpers
{
    public class TeamsHelper
    {
        public static async Task<List<Team>> GetTeamsAsync()
        {
            var teams = await DbHelper.Query<Team>("SELECT * FROM Teams");
            return teams.ToList();
        }
        
    }
}