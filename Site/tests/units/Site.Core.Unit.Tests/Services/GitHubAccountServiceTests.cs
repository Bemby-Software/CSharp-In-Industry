using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.Apis.GitHub;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Configuration;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions.GitHubAccount;
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

        public override void Setup()
        {
            _gitHubAccountRepository = Mocker.GetMock<IGitHubAccountRepository>();
            _gitHubApi = Mocker.GetMock<IGitHubApi>();
            _configuration = Mocker.GetMock<ISiteConfiguration>();
            
            _configuration
                .SetupGet(o => o.GithubApiKeys)
                .Returns(new[] {GitHubApiKey});
            
            _configuration
                .SetupGet(o => o.MasterRepository)
                .Returns(GitHubApiRepository);

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

    }
}