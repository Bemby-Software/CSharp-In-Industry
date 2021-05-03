using System.Net.Http;
using System.Threading.Tasks;

namespace Site.Testing.Common.Helpers
{
    public static class HttpSiteRequestExtensions
    {
        public static Task<HttpResponseMessage> GetParticipantAsync(this HttpClient client, string token, bool includeTeam) 
            => client.GetAsync($"api/participant?token={token}&includeTeam={includeTeam}");
    }
}