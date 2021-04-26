using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Testing.Common.Helpers
{
    public static class TeamsHelper
    {
        public static async Task<List<Team>> GetTeamsAsync()
        {
            var teams = await DbHelper.Query<Team>("SELECT * FROM Teams");
            return teams.ToList();
        }
        
    }
}