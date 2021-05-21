using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Site.Testing.Common.Helpers.Functions
{
    public class AzureFunctionsHelper
    {
        private readonly IFunctionsLogger _logger;
        private readonly string _suffix;
        private readonly List<FunctionsApp> _apps;

        public AzureFunctionsHelper(IFunctionsLogger logger, string suffix = "bin/Debug/netcoreapp3.1")
        {
            _logger = logger;
            _suffix = suffix;
            _apps = new List<FunctionsApp>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location">This location will be recursively search upwards.</param>
        /// <param name="appName">The friendly name for the app.</param>
        public void Start(string location, string appName)
        {
            var fullPath = DirectorySearcher.SearchForFullPath(location, "bin/Debug/netcoreapp3.1");
            
            var process = ProcessExtensions.StartProcess("func", "start", fullPath, s => _logger.WriteInformation(s),
                s => _logger.WriteError(s));

            process.Exited += (sender, args) => throw new Exception("Functions app process existed");

            var app = new FunctionsApp
            {
                Name = appName,
                Process = process,
                FullPath = fullPath
            };
            
            _apps.Add(app);
        }

        public void Stop(string name)
        {
            var app = _apps.FirstOrDefault(o => o.Name == name);

            if (app is null)
                throw new InvalidOperationException($"No functions app with the name: {name} was found");

            app.Process.Kill();
        }
    }
}