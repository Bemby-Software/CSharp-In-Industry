using System;
using Site.Core.Entities;
using Site.Core.Helpers;

namespace Site.Core.Factories
{
    public class TokenFactory : ITokenFactory
    {
        private readonly ITokenHelper _tokenHelper;

        public TokenFactory(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }
        
        public Token Create() => new Token {IsValid = true, Value = _tokenHelper.CreateTokenValue(), CreatedAt = DateTime.Now};
    }
}