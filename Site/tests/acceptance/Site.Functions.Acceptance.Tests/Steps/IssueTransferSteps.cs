using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using FluentAssertions;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Entities;
using Site.Core.Queues.Messages;
using Site.Functions.Acceptance.Tests.Config;
using Site.Functions.Acceptance.Tests.Hooks;
using Site.Testing.Common;
using Site.Testing.Common.Helpers;
using Site.Testing.Common.Models;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Site.Functions.Acceptance.Tests.Steps
{
    [Binding]
    public class IssueTransferSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private const string TransferRepo = "Repo/Transfer";

        private IssueDto _issueToTransfer = new()
        {
            Id = 1,
            Body = "test",
            Number = 1,
            Title = "Test Issue"
        };

        public IssueTransferSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            SetupWireMock();
        }

        private void SetupWireMock()
        {
            
            //Setup Get Issue Request
            GitHubApiHooks.GitHubServer.Given(
                    Request.Create()
                        .WithPath($"/repos/{SiteTestConfiguration.MasterRepository}/issues/1").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBody(JsonSerializer.Serialize(_issueToTransfer)));
            
            //Setup Get Issues Request
            GitHubApiHooks.GitHubServer.Given(
                    Request.Create()
                        .WithPath($"/repos/{SiteTestConfiguration.MasterRepository}/issues").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBody(JsonSerializer.Serialize(new List<IssueDto>(){new IssueDto(), new IssueDto()})));
            
            //Setup Create Issue Request
            GitHubApiHooks.GitHubServer.Given(
                    Request.Create().WithPath($"/repos/{TransferRepo}/issues").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));
            
            GitHubApiHooks.GitHubServer.Given(Request.Create().WithPath("/health").UsingGet()).RespondWith(Response.Create().WithStatusCode(200));
        }
        
        [Given(@"there is team in the database")]
        public async Task GivenThereIsTeamInTheDatabase()
        {
            var id = await DbHelper.TestConnection.InsertAsync(new TestTeam());
            var accountId = await DbHelper.TestConnection.InsertAsync(new GitHubAccount
            {
                Repository = TransferRepo,
                LinkedAt = DateTime.Now,
                TeamId = id,

            });
            _scenarioContext.Set(accountId, "accountId");
        }
        
        [Given(@"there is a issue transfter message on the queue")]
        public async Task GivenThereIsAIssueTransfterMessageOnTheQueue()
        {
            var accountId = _scenarioContext.Get<int>("accountId");
            await QueueHooks.IssueTransferQueue.Add(new IssueTransferMessage
                {
                    IssueNumber = 1, 
                    TransferRepository = TransferRepo, 
                    GitHubAccountId = accountId
                    
                });
        }

        [When(@"the transfer function runs")]
        public async Task WhenTheTransferFunctionRuns()
        {
            await Task.Run(() =>
            {
                int count = 0;
                

                while (count <= 0)
                {
                    var logs = GitHubApiHooks.GitHubServer.FindLogEntries(Request.Create()
                        .UsingPost());
                    count = logs.Count();
                }
            }).TimeoutAfter(TimeSpan.FromSeconds(20));
        }

        [Then(@"a new issue is created on github")]
        public async Task ThenANewIssueIsCreatedOnGithub()
        {
            await Task.Delay(500);
        }

        [Then(@"the issue transfer count for the account is updated")]
        public void ThenTheIssueTransferCountForTheUserIsUpdated()
        {
            var accountId = _scenarioContext.Get<int>("accountId");
            var account = DbHelper.TestConnection.Get<GitHubAccount>(accountId);
            account.IssuesCopied.Should().Be(1);
            account.IsIssueCopyComplete.Should().BeFalse();
        }

        [Then(@"the queue is then empty")]
        public async Task ThenTheQueueIsThenEmpty()
        {
            var result = await QueueHooks.IssueTransferQueue.Get();
            result.Should().BeNull();
        }

        [Given(@"the github api is online")]
        public async Task GivenTheGithubApiIsOnline()
        {
            SetupWireMock();

            await Task.Run(() =>
            {
                var client = new HttpClient();
                var statusCode = HttpStatusCode.Gone;
                while (statusCode != HttpStatusCode.OK)
                {
                    var response = client.GetAsync(new Uri("http://localhost:9000/health")).Result;
                    statusCode = response.StatusCode;
                }
            }).TimeoutAfter(TimeSpan.FromSeconds(5));
        }
    }
}