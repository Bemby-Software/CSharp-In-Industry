using System.Collections.Generic;
using BoDi;
using Site.Core.Queues;
using Site.Core.Queues.Messages;
using Site.Functions.Acceptance.Tests.Queues;
using TechTalk.SpecFlow;

namespace Site.Functions.Acceptance.Tests.Hooks
{
    [Binding]
    public class QueueHooks
    {

        public static IssueTransferQueue IssueTransferQueue;
        
        [BeforeTestRun]
        public static void SetupQueue(IObjectContainer container)
        {
            IssueTransferQueue = new IssueTransferQueue(new TestQueueFactory());
        }
    }
}