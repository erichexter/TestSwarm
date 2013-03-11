using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.NextRun;
using NUnit.Framework;
using Should;
using System;
using System.Linq;

namespace nTestSwarmTests.Application.JobResultsDiff
{
    [TestFixture]
    public class JobResultsDiffTests : IntegrationTestBase
    {
        long job1Id;
        long job2Id;

        protected override void SetUp()
        {
            var browser1 = new UserAgent("test", "test 1", 1);

            var client1 = browser1.SpawnNewClient(null, null);

            Save(browser1, client1);
            
            // create a job with one run and pass it
            WithDbContext(context =>
            {
                SetTime(new DateTime(2011, 1, 1));
                var createJobResult = GetInstance<CreateJobHandler>().Handle(new CreateJob
                {
                    Name = "job",
                    Runs = new[]
                    {
                        new CreateJob.CreateNewRun {Name = "A", Url = "foo"},
                    },
                    MaxRuns = 1
                });
                job1Id = createJobResult.JobId;
                NextRunResult result = GetInstance<NextRunQueryHandler>().Handle(new NextRunQuery(client1.Id));
                GetInstance<CompleteRunHandler>().Handle(new CompleteRun{ClientId = client1.Id, Fail = 0, Total = 1, RunId = result.id});
            });

            // create a new job with one run and fail it
            WithDbContext(context =>
            {
                SetTime(new DateTime(2011, 1, 2));
                var createJobResult = GetInstance<CreateJobHandler>().Handle(new CreateJob
                {
                    Name = "job 2",
                    Runs = new[]
                    {
                        new CreateJob.CreateNewRun {Name = "A", Url = "foo2"},
                    },
                    MaxRuns = 1
                });
                job2Id = createJobResult.JobId;
                NextRunResult result = GetInstance<NextRunQueryHandler>().Handle(new NextRunQuery(client1.Id));
                GetInstance<CompleteRunHandler>().Handle(new CompleteRun { ClientId = client1.Id, Fail = 1, Total = 1, RunId = result.id });
            });

            DbContext().Jobs.Count().ShouldEqual(2);
            DbContext().Jobs.ToArray().All(x => x.Status == JobStatusType.Complete).ShouldBeTrue();

            // async process events
            WithDbContext(context => GetInstance<IJobCompletedEventDistributor>().DistributeAccumlatedJobCompletedEvents());

        }

        [Test]
        public void Should_result_in_one_compare_result_noting_the_new_failure()
        {
            var result = DbContext().RunUserAgentCompareResults.SingleOrDefault();
            if (result == null) Assert.Fail("There are no results in the database");

            result.RunName.ShouldEqual("A");
            result.SourceJobId.ShouldEqual(job1Id);
            result.TargetJobId.ShouldEqual(job2Id);
            result.SourceJobName.ShouldEqual("job");
            result.TargetJobName.ShouldEqual("job 2");
            result.UserAgentName.ShouldEqual("test 1");
            result.Transition.ShouldEqual(RunStatusTransition.NewlyFailing);
        }
    }
}