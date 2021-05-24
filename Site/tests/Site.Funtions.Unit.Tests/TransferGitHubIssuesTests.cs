using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.Apis.GitHub;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Configuration;
using Site.Core.Queues;
using Site.Core.Queues.Messages;
using Site.Core.Services;
using Site.Functions;

namespace Site.Funtions.Unit.Tests
{
    public class TransferGitHubIssuesTests : ServiceUnderTest<TransferGitHubIssues>
    {
        private Mock<IQueue<IssueTransferMessage>> _queue;
        private Mock<IGitHubApi> _gitHubApi;
        private Mock<ILogger> _logger;
        private Mock<ISiteConfiguration> _configuration;
        private Mock<IGitHubAccountService> _gitHubAccountService;
        private const string MasterRepo = "user/master";

        public override void Setup()
        {
            _queue = Mocker.GetMock<IQueue<IssueTransferMessage>>();
            _gitHubApi = Mocker.GetMock<IGitHubApi>();
            _configuration = Mocker.GetMock<ISiteConfiguration>();
            _gitHubAccountService = Mocker.GetMock<IGitHubAccountService>();
            _logger = new Mock<ILogger>();
            
            _configuration.SetupGet(o => o.MasterRepository).Returns(MasterRepo);
        }
        
        [Test]
        public async Task TransferGitHubIssues_NoApiKEys_DoesNotCreateIssue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys).Returns(new string[0]);
            
            //Act
            await sut.Run(null, _logger.Object);

            //Assert
            _gitHubApi.Verify(o => o.CreateIssue(It.IsAny<IssueDto>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _queue.Verify(o => o.Remove(It.IsAny<IssueTransferMessage>()), Times.Never);
            _queue.Verify(o => o.Get(), Times.Never);
            _gitHubAccountService.Verify(o => o.IncrementIssueTransferCount(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task TransferGitHubIssues_ApiKeysNotMessageOnQueue_DoesNotCreateIssue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys).Returns(new[] {"key1"});
            
            //Act
            await sut.Run(null, _logger.Object);

            //Assert
            _gitHubApi.Verify(o => o.CreateIssue(It.IsAny<IssueDto>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _queue.Verify(o => o.Remove(It.IsAny<IssueTransferMessage>()), Times.Never);
            _gitHubAccountService.Verify(o => o.IncrementIssueTransferCount(It.IsAny<int>()), Times.Never);
        }
        
        [Test]
        public async Task TransferGitHubIssues_ApiKeyAndMessageOnQueue_CreatesIssue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys).Returns(new[] {"key1"});

            var item = new IssueTransferMessage()
            {
                IssueNumber = 1,
                TransferRepository = "test/test-repo",
                MessageId = "id",
                PopReceipt = "pop-receipt"
            };

            var issue = new IssueDto();

            _queue.Setup(o => o.Get()).ReturnsAsync(item);

            _gitHubApi.Setup(o => o.GetIssue(item.IssueNumber, MasterRepo , "key1"))
                .ReturnsAsync(issue);

            //Act
            await sut.Run(null, _logger.Object);

            //Assert
            _gitHubApi.Verify(o => o.CreateIssue(issue, item.TransferRepository, "key1"), Times.Once);
            _queue.Verify(o => o.Remove(item), Times.Once);
            _gitHubAccountService.Verify(o => o.IncrementIssueTransferCount(item.GitHubAccountId), Times.Once);
        }
        
        [Test]
        public async Task TransferGitHubIssues_ApiKeysMessageOnQueueExceptionGettingIssueFromMaster_DoesNotCreateIssueAndDoesNotRemoveFromQueue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys).Returns(new[] {"key1"});

            var item = new IssueTransferMessage()
            {
                IssueNumber = 1,
                TransferRepository = "test/test-repo",
                MessageId = "id",
                PopReceipt = "pop-receipt"
            };

            var issue = new IssueDto();

            _queue.Setup(o => o.Get()).ReturnsAsync(item);

            _gitHubApi.Setup(o => o.GetIssue(item.IssueNumber, MasterRepo , "key1"))
                .Throws(new Exception());
            
            //Act
            await sut.Run(null, _logger.Object);

            //Assert
            _gitHubApi.Verify(o => o.CreateIssue(It.IsAny<IssueDto>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _queue.Verify(o => o.Remove(It.IsAny<IssueTransferMessage>()), Times.Never);
            _gitHubAccountService.Verify(o => o.IncrementIssueTransferCount(It.IsAny<int>()), Times.Never);
        }
        
        [Test]
        public async Task TransferGitHubIssues_ApiKeysMessageOnQueueExceptionCreatingIssue_DoesNotRemoveFromQueue()
        {
            //Arrange
            var sut = CreateSut();

            _configuration.SetupGet(o => o.GithubApiKeys).Returns(new[] {"key1"});

            var item = new IssueTransferMessage()
            {
                IssueNumber = 1,
                TransferRepository = "test/test-repo",
                MessageId = "id",
                PopReceipt = "pop-receipt"
            };

            var issue = new IssueDto();

            _queue.Setup(o => o.Get()).ReturnsAsync(item);

            _gitHubApi.Setup(o => o.GetIssue(item.IssueNumber, MasterRepo , "key1"))
                .ReturnsAsync(issue);

            _gitHubApi.Setup(o => o.CreateIssue(issue, item.TransferRepository, "key1"))
                .Throws(new Exception());


            //Act
            await sut.Run(null, _logger.Object);

            //Assert
            _queue.Verify(o => o.Remove(It.IsAny<IssueTransferMessage>()), Times.Never);
            _gitHubAccountService.Verify(o => o.IncrementIssueTransferCount(It.IsAny<int>()), Times.Never);
        }
    }
}