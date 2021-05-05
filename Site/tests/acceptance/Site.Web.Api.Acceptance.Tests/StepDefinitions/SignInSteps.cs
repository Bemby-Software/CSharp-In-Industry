using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Site.Core.DTO.Requests;
using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow;
using Xunit;

namespace Site.Web.Acceptance.StepDefinitions
{
    [Binding]
    public class SignInSteps : IClassFixture<SiteWebApplicationFactory>
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;

        public SignInSteps(ScenarioContext scenarioContext, SiteWebApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _client = factory.CreateClient();
        }

        [Given(@"a user with the email '(.*)' is signed up")]
        public async Task GivenAUserWithTheEmailIsSignedUp(string email)
        {
            var token = await FlowHelpers.SignUpTeamWithUserAndGetTokensAsync(email);
            _scenarioContext.Set(token, "token-value");
            _scenarioContext.Set(email, "email");
        }

        [When(@"a user signs in with there details")]
        public async Task WhenAUserSignsInWithThereDetails()
        {
            var request = new SignInRequest()
            {
                Email = _scenarioContext.Get<string>("email"),
                Token = _scenarioContext.Get<string>("token-value")
            };
            var response = await _client.PostAsJsonAsync("api/participant/signin", request);
            _scenarioContext.Set(response);
        }

        [Given(@"a user with the email '(.*)' and the token value of '(.*)' tries to sign in")]
        public async Task GivenAUserWithTheEmailAndTheTokenValueOfTriesToSignIn(string email, string token)
        {
            var request = new SignInRequest()
            {
                Email = email,
                Token = token
            };
            var response = await _client.PostAsJsonAsync("api/participant/signin", request);
            _scenarioContext.Set(response);
        }

        [When(@"they are not signed up")]
        public void WhenTheyAreNotSignedUp()
        { 
            
        }
    }
}