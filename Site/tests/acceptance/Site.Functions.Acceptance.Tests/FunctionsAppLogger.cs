using System.Diagnostics;
using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow.Infrastructure;

namespace Site.Web.Acceptance.Functions
{
    public class FunctionsAppLogger : IFunctionsLogger
    {
        
        public void WriteInformation(string info)
        {
            Debug.WriteLine($"APP INFO {info}");
        }

        public void WriteError(string error)
        {
            Debug.WriteLine($"APP INFO {error}");
        }
    }
}