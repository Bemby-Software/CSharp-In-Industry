using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.Functions.Steps
{
    [Binding]
    public class IssueTransferSteps
    {
        
        
        [Given(@"there is team in the database")]
        public void GivenThereIsTeamInTheDatabase()
        {
            
        }
        
        [Given(@"there is a issue transfter message on the queue")]
        public void GivenThereIsAIssueTransfterMessageOnTheQueue()
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"the transfer function runs")]
        public void WhenTheTransferFunctionRuns()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"a new issue is created on github")]
        public void ThenANewIssueIsCreatedOnGithub()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the issue transfer count for the user is updated")]
        public void ThenTheIssueTransferCountForTheUserIsUpdated()
        {
            ScenarioContext.StepIsPending();
        }
    }
}