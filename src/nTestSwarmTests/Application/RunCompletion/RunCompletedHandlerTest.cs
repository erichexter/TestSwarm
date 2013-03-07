using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Infrastructure.Threading;

namespace nTestSwarmTests.Application.RunCompletion
{
    [TestFixture]
    public class RunCompletedHandlerTest : IntegrationTestBase
    {
        [Test]
        public void should_record_a_successful_run()
        {
            var userAgent = new UserAgent("browser", "Browser", null);
            var job = new Job("job");
            var run = new Run(job, "run", "http://foo");
            var runUserAgent = new RunUserAgent(run, userAgent);
            Client client = userAgent.SpawnNewClient("ip", "os");
            run.BeginClientRun(client);

            Save(userAgent, job, run, runUserAgent, client, run.ClientRuns.Single());

            var runCompleted = new CompleteRun
                {
                    Client_Id = client.Id,
                    Run_id =  run.Id,
                    Total = 10
                };

//            WithDbContext(context => GetInstance<CompleteRunHandler>().Handle(runCompleted));

            WithDbContext(context =>
                              {
                                  GetInstance<CompleteRunHandler>().Handle(runCompleted);
                                  var foundRun = context.Find<Run>(run.Id);

                                  var single = foundRun.RunUserAgents.Single();

                                  var clientRun = single.Run.ClientRuns.Where(x => x.Client.UserAgent.Id == single.UserAgent.Id).FirstOrDefault() ?? new ClientRun();

                                  clientRun.IndicatesSuccess().ShouldBeTrue();
                                  single.RemainingRuns.ShouldEqual(0);
                                  single.RunStatus.ShouldEqual(RunStatusType.Finished);
                              });
        }

        [Test,Ignore]
        public void should_record_a_fail_run()
        {
            //TODO: fix test
            //var userAgent = new UserAgent("browser", "Browser", null);
            //var job = new JobId("job");
            //var run = new Run(job, "run", "http://foo");
            //var runUserAgent = new RunUserAgent(run, userAgent, 2);
            //Client client = userAgent.SpawnNewClient("ip", "os");
            //run.BeginClientRun(client);

            //Save(userAgent, job, run, runUserAgent, client, run.ClientRuns.Single());

            //var runCompleted = new CompleteRun
            //{
            //    Client_Id = client.Id,
            //    Run_id = run.Id,
            //    Total = 10,
            //    Fail = 1
            //};

            //var nTestSwarmContext = DbContext();

            //new CompleteRunHandler(nTestSwarmContext, new EventPublisherStub(new ThreadPool())).Handle(runCompleted);

            //WithDbContext(context =>
            //{
            //    var foundRun = context.Find<Run>(run.Id);

            //    var single = foundRun.RunUserAgents.Single();

            //    var clientRun = single.Run.ClientRuns.Where(x => x.Client.UserAgent.Id == single.UserAgent.Id).FirstOrDefault() ?? new ClientRun();

            //    clientRun.IndicatesFailureOrProblem().ShouldBeTrue();
            //    single.RemainingRuns.ShouldEqual(1);
            //    single.RunStatus.ShouldEqual(RunStatusType.Running);
            //});
        }
    }
}