using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions.General;
using Site.Core.Exceptions.Participants;
using Site.Core.Helpers;
using Site.Core.Services;

namespace Site.Core.Unit.Tests.Services
{
    public class ParticipantServiceTests : ServiceUnderTest<IParticipantService, ParticipantService>
    {
        private Mock<IEmailHelper> _emailHelper;
        private Mock<IParticipantRepository> _participantsRepository;

        public override void Setup()
        {
            _emailHelper = Mocker.GetMock<IEmailHelper>();
            _participantsRepository = Mocker.GetMock<IParticipantRepository>();
        }

        [Test]
        public void ValidateParticipant_EmptyForename_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var participant = new Participant();

            //Act
            //Assert
            Assert.ThrowsAsync<ParticipantForenameRequiredException>(() => sut.ValidateParticipant(participant));
        }
        
        [Test]
        public void ValidateParticipant_EmptySurname_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var participant = new Participant(){Forename = "Joe"};

            //Act
            //Assert
            Assert.ThrowsAsync<ParticipantSurnameRequiredException>(() => sut.ValidateParticipant(participant));
        }

        [Test] 
        public void ValidateParticipant_InvalidEmail_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var participant = new Participant()
            {
                Forename = "Joe",
                Surname = "Bloggs",
                Email = "test@test.com"
            };

            _emailHelper.Setup(o => o.IsValidEmail(participant.Email))
                .Returns(false);

            //Act
            //Assert
            Assert.ThrowsAsync<InvalidEmailException>(() => sut.ValidateParticipant(participant));
        }
        
        [Test] 
        public void ValidateParticipant_InUseEmail_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var participant = new Participant()
            {
                Forename = "Joe",
                Surname = "Bloggs",
                Email = "test@test.com"
            };

            _emailHelper
                .Setup(o => o.IsValidEmail(participant.Email))
                .Returns(true);
            
            _participantsRepository
                .Setup(o => o.IsEmailInUseAsync(participant.Email))
                .ReturnsAsync(true);

            //Act
            //Assert
            Assert.ThrowsAsync<EmailInUseException>(() => sut.ValidateParticipant(participant));
        }
        
        [Test] 
        public async Task ValidateParticipant_ValidParticipant_DoesNotThrow()
        {
            //Arrange
            var sut = CreateSut();

            var participant = new Participant()
            {
                Forename = "Joe",
                Surname = "Bloggs",
                Email = "test@test.com"
            };

            _emailHelper
                .Setup(o => o.IsValidEmail(participant.Email))
                .Returns(true);
            
            _participantsRepository
                .Setup(o => o.IsEmailInUseAsync(participant.Email))
                .ReturnsAsync(false);

            //Act
            //Assert
            await sut.ValidateParticipant(participant);
        }
        
        [Test]
        public void IsEmailInOkAsync_InvalidEmail_Throws()
        {
            //Arrange
            var sut = CreateSut();

            _emailHelper.Setup(o => o.IsValidEmail(It.IsAny<string>())).Returns(false);

            //Act
            Assert.ThrowsAsync<InvalidEmailException>(() => sut.IsEmailInOkAsync("test@test.com"));
        }
        
        [Test]
        public void IsEmailInOkAsync_InUseEmail_Throws()
        {
            //Arrange
            var sut = CreateSut();

            _emailHelper.Setup(o => o.IsValidEmail(It.IsAny<string>())).Returns(true);

            _participantsRepository.Setup(o => o.IsEmailInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            Assert.ThrowsAsync<EmailInUseException>(() => sut.IsEmailInOkAsync("test@test.com"));
        }
        
        [Test]
        public async Task IsEmailInOkAsync_OkEmail_DoesNotThrows()
        {
            //Arrange
            var sut = CreateSut();

            _emailHelper.Setup(o => o.IsValidEmail(It.IsAny<string>())).Returns(true);

            _participantsRepository.Setup(o => o.IsEmailInUseAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            //Act
            await sut.IsEmailInOkAsync("test@test.com");
        }

        [Test]
        public async Task SignInAsync_ValidDetails_DoesNotThrow()
        {
            //Arrange
            var sut = CreateSut();

            var email = "test@test.com";
            var token = "token123";

            _participantsRepository.Setup(o => o.AreSignInDetailsValidAsync(email, token))
                .ReturnsAsync(true);

            //Act
            //Assert
            await sut.SignInAsync(email, token);
        }
        
        [Test]
        public void SignInAsync_InValidDetails_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var email = "test@test.com";
            var token = "token123";

            //Act
            //Assert
            Assert.ThrowsAsync<ParticipantsSignInDetailInvalidException>(() => sut.SignInAsync(email, token));
        }
        
        
    }   
}