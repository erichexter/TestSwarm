using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Commands.JobResetting;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.NextRun;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;
using NUnit.Framework;
using Should;
using System.Linq;
using nTestSwarmTests.Application.JobResultsDiff;

namespace nTestSwarmTests.Application.JobReset
{
    [TestFixture]
    public class ResetJobHandlerTests : IntegrationTestBase
    {
        Client _client2;
        Client _client1;
        Client _client3;

        protected override void SetUp()
        {
            StructureMap.Inject(typeof(IEventPublisher), new NoOpEventPublisher());

            var browser1 = new UserAgent("test", "test 1", 1);
            var browser2 = new UserAgent("test", "test 2", 2);
            var browser3 = new UserAgent("another", "another 1", 3);

            _client1 = browser1.SpawnNewClient(null, null);
            _client2 = browser2.SpawnNewClient(null, null);
            _client3 = browser3.SpawnNewClient(null, null);

            Save(browser1, browser2, browser3, _client1, _client2, _client3);

            WithDbContext(context =>
                              {
                                  var userAgentCache = new UserAgentCache(() => context);
                                  var newJobCreator = new CreateJobHandler(context, userAgentCache);
                                  newJobCreator.Handle(new CreateJob
                                      {
                                          Name = "job",
                                          Runs = new[]
                                              {
                                                  new CreateJob.CreateNewRun {Name = "C", Url = "foo"},
                                                  new CreateJob.CreateNewRun {Name = "A", Url = "foo"},
                                                  new CreateJob.CreateNewRun {Name = "B", Url = "foo"},
                                                  new CreateJob.CreateNewRun {Name = "D", Url = "foo"},
                                              }
                                      });
                                  context.SaveChanges();
                                  context.All<Client>().Count().ShouldEqual(3);
                              });

            WithDbContext(context =>
                              {
                                  for (int i = 0; i < 4; i++)
                                  {
                                      foreach (var client in new[]{_client1, _client2, _client3})
                                      {
                                          var nextRunQuery = new NextRunQuery(client.Id);
                                          var handler = GetInstance<NextRunQueryHandler>();
                                          var nextRunResult = handler.Handle(nextRunQuery);

                                          var runCompleted = new CompleteRun {Client_Id = client.Id, Run_id = nextRunResult.id, Total = 1};
                                          GetInstance<CompleteRunHandler>().Handle(runCompleted);
                                      }
                                  }
                              });

            long jobId = 0;

            WithDbContext(context =>
                              {
                                  var job = context.Jobs.Single();
                                  jobId = job.Id;

                                  job.Status.ShouldEqual(JobStatusType.Complete);

                              });

            WithDbContext(context =>
                              {
                                  var resetJob = new ResetJob(jobId);

                                  var resetJobHandler = new ResetJobHandler(context, S<IOutputCacheInvalidator>());

                                  resetJobHandler.Handle(resetJob);
                              });

            

        }

        [Test]
        public void TEST()
        {
            WithDbContext(context =>
            {
                var job = context.Jobs.Single();

                job.Status.ShouldEqual(JobStatusType.Created);
                job.Runs.All(x => x.RunStatus == RunStatusType.NotStarted).ShouldBeTrue();
                job.Runs.SelectMany(x => x.RunUserAgents).All(x => x.RunStatus == RunStatusType.NotStarted).ShouldBeTrue();
            });


        }
    }
}