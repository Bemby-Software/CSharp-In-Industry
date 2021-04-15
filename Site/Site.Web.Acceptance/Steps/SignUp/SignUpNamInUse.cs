using System.ComponentModel;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.Steps.SignUp
{
    [Binding]
    public class SignUpNamInUseStep
    {

        [Given(@"a team tries to sign up")]
        public void GivenATeamTriesToSignUp()
        {
            ScenarioContext.StepIsPending();
        }
        
        [When(@"the team name is in use")]
        public void WhenTheTeamNameIsInUse()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"an error should be returned")]
        public void ThenAnErrorShouldBeReturned()
        {
            ScenarioContext.StepIsPending();
        }

        
    }
}