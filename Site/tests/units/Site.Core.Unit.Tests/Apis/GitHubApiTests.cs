using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.Apis;
using Site.Core.Apis.GitHub;
using Site.Core.Apis.GitHub.DTO;

namespace Site.Core.Unit.Tests.Apis
{
    public class GitHubApiTests : ServiceUnderTest<IGitHubApi, GitHubApi>
    {
        private Mock<IHttpClient> _client;
        private const string Repository = "user010/test";
        private const string ApiKey = "HDGFHJDSKFY&*2342345";


        public override void Setup()
        {
            _client = Mocker.GetMock<IHttpClient>();
            _client.SetupGet(o => o.DefaultRequestHeaders).Returns(new HttpRequestMessage().Headers);
        }

        [Test]
        public async Task GetIssue_SuccessfulRequest_ReturnsIssueDto()
        {
            //Arrange
            var sut = CreateSut();

            var label = new LabelDto()
            {
                Id = 10,
                Color = "blue",
                Name = "test-label",
                Description = "A label description"
            };

            var issue = new IssueDto
            {
                Id = 1,
                Number = 2,
                Title = "Test Issue",
                Body = "This is a test issue body",
                Labels = new List<LabelDto>
                {
                    label
                }
            };

            var response = new HttpResponseMessage()
            {
                Content = JsonSnakeCase(issue),
                StatusCode = HttpStatusCode.OK
            };

            var issueNumber = 2;
            
            _client.Setup(o => o.GetAsync($"/repos/{Repository}/issues/{issueNumber}")).ReturnsAsync(response);


            //Act
            var result = await sut.GetIssue(issueNumber, Repository, ApiKey);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(issue.Id, result.Id);
            Assert.AreEqual(issue.Number, result.Number);
            Assert.AreEqual(issue.Title, result.Title);
            Assert.AreEqual(issue.Body, result.Body);
            Assert.AreEqual(issue.Labels.Count(), result.Labels.Count());
            var resultLabel = result.Labels.First();
            Assert.AreEqual(resultLabel.Id, label.Id);
            Assert.AreEqual(resultLabel.Name, label.Name);
            Assert.AreEqual(resultLabel.Description, label.Description);
            Assert.AreEqual(resultLabel.Color, label.Color);
        }

        private StringContent JsonSnakeCase<T>(T data) =>
            new(JsonSerializer.Serialize(data, new(){PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance}), Encoding.UTF8, "application/json");
        
    }
}