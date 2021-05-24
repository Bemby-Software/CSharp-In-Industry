using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Site.Testing.Common.Helpers.Functions
{
    public class AzureFunctionsHelper
    {
        private readonly IFunctionsLogger _logger;
        private readonly List<FunctionsApp> _apps;
        
        private const string AcceptanceTestingEnvironment = "AcceptanceTesting";
        private const string FunctionsEnvironment = "FUNCTIONS_ENVIRONMENT";

        public AzureFunctionsHelper(IFunctionsLogger logger)
        {
            _logger = logger;
            _apps = new List<FunctionsApp>();
        }

        /// <summary>
        /// Builds & Starts a functions app sets FUNCTIONS_ENVIRONMENT = AcceptanceTesting
        /// Use this in the functions app to override any settings needed.
        /// </summary>
        /// <param name="location">This location will be recursively search upwards.</param>
        /// <param name="appName">The friendly name for the app.</param>
        public void Start(string location, string appName)
        {
            var functionsSourcePath = DirectorySearcher.SearchForFullPath(location);

            var process = ProcessExtensions.StartProcess(
                "func", 
                "start", 
                functionsSourcePath, 
                s => _logger.WriteInformation(s),
                s => _logger.WriteError(s), 
                (FunctionsEnvironment, AcceptanceTestingEnvironment));

            var app = new FunctionsApp
            {
                Name = appName,
                Process = process,
                FullPath = functionsSourcePath
            };
            
            _apps.Add(app);
        }

        public void Stop(string name)
        {
            var app = _apps.FirstOrDefault(o => o.Name == name);

            if (app is null)
                throw new InvalidOperationException($"No functions app with the name: {name} was found");

            app.Process.Kill();
            app.Process.WaitForExit();
            app.Process.Dispose();
        }
    }
}