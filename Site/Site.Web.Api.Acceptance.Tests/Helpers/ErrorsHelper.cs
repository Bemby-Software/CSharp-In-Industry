using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Site.Core.DTO.Common;
using Site.Core.Exceptions;

namespace Site.Web.Acceptance.Helpers
{
    public static class ErrorsHelper
    {
        public static async Task VerifyErrorIs<T>(HttpResponseMessage response) where T : ExceptionBase, new()
        {
            var exception = new T();

            var error = await GetError(response);

            error.Should().NotBeNull();
            error.Code.Should().Be(exception.Code);
            error.Reason.Should().Be(exception.Reason);
            error.IsUserFriendly.Should().Be(exception.IsUserFriendly);
        }

        public static async Task<ErrorDto> GetError(HttpResponseMessage response) 
            => await response.Content.ReadFromJsonAsync<ErrorDto>();
    }
}