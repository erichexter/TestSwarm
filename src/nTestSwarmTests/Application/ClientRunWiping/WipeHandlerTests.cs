using nTestSwarm.Application.Commands.DataCleanup;
using nTestSwarm.Application.Domain;
using NUnit.Framework;
using Should;
using System;
using System.Linq;

namespace nTestSwarmTests.Application.ClientRunWiping
{
    [TestFixture]
    public class WipeHandlerTests : IntegrationTestBase
    {
        WipeHandler wipeHandler;

        protected override void TestFixtureSetUp()
        {
            var now = new DateTime(2000, 3, 3, 3, 15, 1);
            var fiveMinutesAgo = now.AddMinutes(-5).AddSeconds(-1);

            SetTime(fiveMinutesAgo);

            // set up a clientrun that is in progress but has been running for five minutes
            var userAgent = new UserAgent("browser", "Browser", null);
            var job = new Job("job");
            var run = new Run(job, "run", "http://foo");
            var runUserAgent = new RunUserAgent(run, userAgent);
            Client client = userAgent.SpawnNewClient("ip", "os");
            run.BeginClientRun(client);

            Save(userAgent, job, run, runUserAgent, client, run.ClientRuns.Single());

            // create a wipe handler in the present
            wipeHandler = new WipeHandler(DbContext(), SetTime(now));

            // wipe
            wipeHandler.Handle(null);
        }

        [Test]
        public void Should_wipe_old_client_runs()
        {
            WithDbContext(context => context.All<ClientRun>().ShouldBeEmpty());
        }
    }

    [TestFixture]
    public class WipeHandlerTests_newer_client_runs: IntegrationTestBase
    {
        protected override void TestFixtureSetUp()
        {
            var now = new DateTime(2000, 3, 3, 3, 15, 0);
            var almost5MinutesAgo = now.AddMinutes(-5).AddSeconds(1);

            SetTime(almost5MinutesAgo);

            // set up a clientrun that is in progress but has been running for five minutes
            var userAgent = new UserAgent("browser", "Browser", null);
            var job = new Job("job");
            var run = new Run(job, "run", "http://foo");
            var runUserAgent = new RunUserAgent(run, userAgent);
            Client client = userAgent.SpawnNewClient("ip", "os");
            run.BeginClientRun(client);

            Save(userAgent, job, run, runUserAgent, client, run.ClientRuns.Single());

            // create a wipe handler in the present
            var wipeHandler = new WipeHandler(DbContext(), SetTime(now));


            // wipe
            wipeHandler.Handle(null);
        }

        [Test]
        public void Should_not_wipe_newer_client_runs()
        {
            WithDbContext(context => context.All<ClientRun>().Count().ShouldEqual(1));
        }
    }
}