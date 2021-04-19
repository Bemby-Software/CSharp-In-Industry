using System;
using System.Text;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.Helpers;

namespace Site.Core.Unit.Tests.Helpers
{
    public class TokenHelperTests : ServiceUnderTest<ITokenHelper, TokenHelper>
    {
        public override void Setup()
        {
            
        }

        [Test]
        public void CreateTokenValue_Always_EncodesValidGuid()
        {
            //Arrange
            var sut = CreateSut();
            
            //Act
            var token = sut.CreateTokenValue();
            
            //Assert
            Assert.IsTrue(Guid.TryParse(Encoding.UTF8.GetString(Convert.FromBase64String(token)), out var guid));
        }
    }
}