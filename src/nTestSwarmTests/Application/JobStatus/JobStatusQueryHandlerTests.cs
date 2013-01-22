using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Application.Repositories;

namespace nTestSwarmTests.JobStatus
{
    [TestFixture]
    public class JobStatusQueryHandlerTests : IntegrationTestBase
    {
        Client _client1;
        Client _client2;
        Client _client3a;
        Client _client3b;
        CreateJobResult _createJobResult;

        protected override void SetUp()
        {
            var browser1 = new UserAgent("test", "test 1", 1);
            var browser2 = new UserAgent("test", "test 2", 2);
            var browser3 = new UserAgent("another", "another 1", 3);

            _client1 = browser1.SpawnNewClient(null, null);
            _client2 = browser2.SpawnNewClient(null, null);
            _client3a = browser3.SpawnNewClient(null, null);
            _client3b = browser3.SpawnNewClient(null, null);

            Save(browser1, browser2, browser3, _client1, _client2, _client3a, _client3b);

            WithDbContext(context =>
                              {
                                  _createJobResult =
                                      new CreateJobHandler(context, new UserAgentCache(() => context)).Handle(new CreateJob
                                          {
                                              Name = "job",
                                              SuiteId = "testSuite",
                                              Runs = new[]
                                                  {
                                                      new CreateJob.CreateNewRun {Name = "C", Url = "foo"},
                                                      new CreateJob.CreateNewRun {Name = "A", Url = "foo"},
                                                      new CreateJob.CreateNewRun {Name = "B", Url = "foo"},
                                                      new CreateJob.CreateNewRun {Name = "D", Url = "foo"},
                                                  }
                                          });
                                  context.SaveChanges();
                                  context.All<Client>().Count().ShouldEqual(4);
                              });
        }

        [Test]
        public void Show_query()
        {
            long id = 0;

            WithDbContext(context =>
                              {
                                  context.Jobs.Count().ShouldEqual(1);
                                  id = context.Jobs.Single().Id;
                              });
            WithDbContext(context =>
                              {
                                  var query = new JobStatusQueryHandler(context);

                                  query.Handle(new JobStatusQuery(id));
                                  
                              });
        }
    }
}