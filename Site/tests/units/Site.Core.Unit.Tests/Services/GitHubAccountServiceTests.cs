using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Nytte.Testing;
using Site.Core.Apis.GitHub;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Configuration;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions.GitHubAccount;
using Site.Core.Exceptions.Teams;
using Site.Core.Queues;
using Site.Core.Queues.Messages;
using Site.Core.Services;

namespace Site.Core.Unit.Tests.Services
{
    public class GitHubAccountServiceTests : ServiceUnderTest<IGitHubAccountService, GitHubAccountService>
    {
        private Mock<IGitHubAccountRepository> _gitHubAccountRepository;
        private Mock<IGitHubApi> _gitHubApi;
        private Mock<ISiteConfiguration> _configuration;

        private const string GitHubApiKey = "api-key";
        private const string GitHubApiRepository = "master-repo";
        private List<IssueDto> _issues;
        private Mock<ITeamService> _teamService;
        private Mock<IQueue<IssueTransferMessage>> _queueService;

        public override void Setup()
        {
            _gitHubAccountRepository = Mocker.GetMock<IGitHubAccountRepository>();
            _gitHubApi = Mocker.GetMock<IGitHubApi>();
            _configuration = Mocker.GetMock<ISiteConfiguration>();
            _teamService = Mocker.GetMock<ITeamService>();
            
            _configuration
                .SetupGet(o => o.GithubApiKeys)
                .Returns(new[] {GitHubApiKey});
            
            _configuration
                .SetupGet(o => o.MasterRepository)
                .Returns(GitHubApiRepository);

            _queueService = Mocker.GetMock<IQueue<IssueTransferMessage>>();
            

            _issues = new List<IssueDto>
            {
                new()
                {

                },
                new()
                {
                    
                }
            };

            _gitHubApi.Setup(o => o.GetIssues(GitHubApiRepository, GitHubApiKey))
                .ReturnsAsync(_issues);
        }
        
        [Test]
        public void IncrementIssueTransferCount_NoAccount_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var id = 2;

            //Act
            //Assert
            Assert.ThrowsAsync<GitHubAccountNotFoundException>(() => sut.IncrementIssueTransferCount(id));
        }

        [Test]
        public void IncrementIssueTransferCount_AlreadyCompleted_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var id = 2;

            var account = new GitHubAccount {IsIssueCopyComplete = true};


            _gitHubAccountRepository.Setup(o => o.GetAsync(2))
                .ReturnsAsync(account);

            //Act
            //Assert
            Assert.ThrowsAsync<IssueCopyAlreadyCompleteException>(() => sut.IncrementIssueTransferCount(id));
        }

        [Test]
        public async Task IncrementIssueTransferCount_ValidAccountNotCompleted_IncrementsIfNotComplete()
        {
            //Arrange
            var sut = CreateSut();

            var id = 2;

            var account = new GitHubAccount
            {
                IsIssueCopyComplete = false,
                IssuesCopied = 0,
            };


            _gitHubAccountRepository.Setup(o => o.GetAsync(2))
                .ReturnsAsync(account);
            
            
            //Act
            await sut.IncrementIssueTransferCount(id);
            
            //Assert
            _gitHubAccountRepository
                .Verify(o => o.
                    UpdateAsync(
                        It.Is<GitHubAccount>( 
                            a => a.IsIssueCopyComplete == false 
                                && a.IssuesCopied == 1)));
        }

        
        [Test]
        public async Task IncrementIssueTransferCount_ValidAccountNotCompleted_IncrementsAndCompletesIf()
        {
            //Arrange
            var sut = CreateSut();

            var id = 2;

            var account = new GitHubAccount
            {
                IsIssueCopyComplete = false,
                IssuesCopied = 1,
            };


            _gitHubAccountRepository.Setup(o => o.GetAsync(2))
                .ReturnsAsync(account);
            
            
            //Act
            await sut.IncrementIssueTransferCount(id);
            
            //Assert
            _gitHubAccountRepository
                .Verify(o => o.
                    UpdateAsync(
                        It.Is<GitHubAccount>( 
                            a => a.IsIssueCopyComplete == true 
                                 && a.IssuesCopied == 2)));
        }


        [Test]
        public async Task Assign_WithAccessGetsIssues_SavesRepoAndAddsIssueToQueue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys)
                .Returns(new[] {"key1", "key2"});

            var master = "master/repo";
            _configuration.SetupGet(o => o.MasterRepository).Returns(master);

            _teamService.Setup(o => o.IsValid(1)).ReturnsAsync(true);

            var issues = new List<IssueDto>()
            {
                new()
                {
                    Number = 1,
                },
                new()
                {
                    Number = 5,
                }
            };
            
            _gitHubAccountRepository
                .Setup(o => 
                    o.CreateAsync(It.Is<GitHubAccount>(a => a.Repository == GitHubApiRepository && a.TeamId == 1)))
                .ReturnsAsync(5);

            _gitHubApi.Setup(o => o.GetIssues(master, _configuration.Object.GithubApiKeys.First()))
                .ReturnsAsync(issues);
            
            //Act
            await sut.Assign(GitHubApiRepository, 1);

            //Assert
            foreach (var key in _configuration.Object.GithubApiKeys)
                _gitHubApi.Verify(o => o.CreateIssue(It.IsAny<IssueDto>(), GitHubApiRepository, key));

            foreach (var issueDto in issues)
                _queueService.Verify(o => o.Add(It.Is<IssueTransferMessage>(
                    m => m.IssueNumber == issueDto.Number &&
                         m.TransferRepository == GitHubApiRepository &&
                         m.GitHubAccountId == 5)));
        }


        [Test]
        public void Assign_InvalidTeamId_Throws()
        {
            //Arrange
            var sut = CreateSut();
            
            //Act
            //Assert
            Assert.ThrowsAsync<TeamNotFoundException>(() => sut.Assign(GitHubApiRepository, 1));
        }
        
        [Test]
        public void Assign_AccountWithAccessAndAccountWithout_Throws()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys)
                .Returns(new[] {"key1", "key2"});

            _teamService.Setup(o => o.IsValid(1)).ReturnsAsync(true);

            _gitHubApi.Setup(o =>
                    o.CreateIssue(It.IsAny<IssueDto>(), GitHubApiRepository, _configuration.Object.GithubApiKeys[1]))
                .Throws(new Exception());
            
            //Act
            //Assert
            Assert.ThrowsAsync<WriteAccessNeededException>(() => sut.Assign(GitHubApiRepository, 1));
            _gitHubAccountRepository.Verify(o => o.CreateAsync(It.IsAny<GitHubAccount>()), Times.Never);
        }
        
        [Test]
        public void Assign_AccountWithoutAccess_Throws()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys)
                .Returns(new[] {"key1", "key2"});

            _teamService.Setup(o => o.IsValid(1)).ReturnsAsync(true);

            _gitHubApi.Setup(o =>
                    o.CreateIssue(It.IsAny<IssueDto>(), GitHubApiRepository, _configuration.Object.GithubApiKeys[1]))
                .Throws(new Exception());
            
            _gitHubApi.Setup(o =>
                    o.CreateIssue(It.IsAny<IssueDto>(), GitHubApiRepository, _configuration.Object.GithubApiKeys[0]))
                .Throws(new Exception());
            
            //Act
            //Assert
            Assert.ThrowsAsync<WriteAccessNeededException>(() => sut.Assign(GitHubApiRepository, 1));
            _gitHubAccountRepository.Verify(o => o.CreateAsync(It.IsAny<GitHubAccount>()), Times.Never);
        }

    }
}