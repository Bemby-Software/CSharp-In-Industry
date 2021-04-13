using System.Linq;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Integration.Tests.Helpers
{
    public class TokensHelper
    {
        public static async Task<Token> GetTokenForParticipantAsync(int participantId)
        {
            var tokens = await DbHelper.Query<Token>(@"SELECT * FROM Tokens WHERE ParticipantId = @Id",
                new {Id = participantId});
            
            return tokens.FirstOrDefault();
        }
    }
}