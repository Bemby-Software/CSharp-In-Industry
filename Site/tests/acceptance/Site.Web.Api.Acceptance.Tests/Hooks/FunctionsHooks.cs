using System.Threading.Tasks;
using Corvus.Testing.AzureFunctions.SpecFlow;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.Hooks
{
    [Binding]
    public class FunctionsHooks
    {
        [BeforeScenario]
        public static Task StartFunctionsApp(ScenarioContext context)
        {
            var configuration = FunctionsBindings.GetFunctionConfiguration(context);
            var controller = FunctionsBindings.GetFunctionsController(context);

            return controller.StartFunctionsInstance("src/Site.Functions", 5050, "netcoreapp3.1", configuration: configuration);
        }
    }
}