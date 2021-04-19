using System;
using System.Text;
using Moq;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.Factories;
using Site.Core.Helpers;

namespace Site.Core.Unit.Tests.Factories
{
    public class TokenFactoryTests : ServiceUnderTest<ITokenFactory, TokenFactory>
    {
        private Mock<ITokenHelper> _tokenHelper;

        public override void Setup()
        {
            _tokenHelper = Mocker.GetMock<ITokenHelper>();
        }

        [Test]
        public void Create_Always_CreatesToken()
        {
            //Arrange
            var sut = CreateSut();

            var tokenValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            _tokenHelper.Setup(o => o.CreateTokenValue()).Returns(tokenValue);

            //Act
            var token = sut.Create();

            //Assert
            Assert.AreEqual(tokenValue, token.Value);
            Assert.AreEqual(true, token.IsValid);
            Assert.IsTrue(token.CreatedAt > DateTime.Now.AddSeconds(-3) && token.CreatedAt < DateTime.Now.AddSeconds(1));
        }
    }
}