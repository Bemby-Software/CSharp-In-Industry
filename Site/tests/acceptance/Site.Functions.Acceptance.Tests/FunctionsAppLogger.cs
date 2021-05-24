using System;
using System.Diagnostics;
using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow.Infrastructure;

namespace Site.Web.Acceptance.Functions
{
    public class FunctionsAppLogger : IFunctionsLogger
    {
        
        public void WriteInformation(string info)
        {
            Console.WriteLine($"APP INFO {info}");
        }

        public void WriteError(string error)
        {
            Console.WriteLine($"APP INFO {error}");
        }
    }
}