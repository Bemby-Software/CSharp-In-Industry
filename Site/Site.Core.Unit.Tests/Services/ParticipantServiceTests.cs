using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
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
    }   
}