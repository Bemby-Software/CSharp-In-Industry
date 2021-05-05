using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Docker.DotNet.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Site.Testing.Common.Helpers;
using Timer = System.Threading.Timer;

namespace Site.Core.Integration.Tests
{
    [SetUpFixture]
    public class TestStartup
    {
        [OneTimeSetUp]
        public async Task SetupDatabaseAsync()
        {
            
            await SqlServerContainer.StartAsync();
            var settings = TestConfiguration.GetConfiguration();

            await DbHelper.EnsureStarted(settings.DbServerConnectionString, TimeSpan.FromSeconds(60));
            
            await DbHelper.ReCreateDatabase();
        }
    }
}