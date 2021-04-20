using System;
using System.Net.Http;
using System.Resources;

namespace Site.Testing.Common
{
    public static class HttpExtensions
    {
        public static bool ReadContentAsBoolean(this HttpResponseMessage responseMessage)
        {
            var task = responseMessage.Content.ReadAsStringAsync();
            task.Wait();
            var content = task.Result;

            return content.ToLower() switch
            {
                "true" => true,
                "false" => false,
                _ => throw new Exception($"content is not a boolean: {content}")
            };
        }
    }
}