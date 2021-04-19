using System;
using System.Text;
using System.Text.Unicode;

namespace Site.Core.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public string CreateTokenValue() 
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
    }
}