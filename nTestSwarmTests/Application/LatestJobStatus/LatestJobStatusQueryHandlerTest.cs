using System;
using NUnit.Framework;
using Rhino.Mocks;
using nTestSwarm.Application;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Application.Services;

namespace nTestSwarmTests.Application.LatestJobStatus
{
    [TestFixture]
    public class LatestJobStatusQueryHandlerTest : TestBase
    {
        [Test]
        public void Should_get_latest_job()
        {
            var mock = S<IHandler<JobStatusQuery, JobStatusResult>>();

            var dataBase = S<IDataBase>();

            var older = new StubJob(new DateTime(2001, 1, 1)) {Id = 1};
            var old = new StubJob(new DateTime(2001, 1, 2)) {Id = 2};
            var @new = new StubJob(new DateTime(2001, 1, 3)) {Id = 3};

            dataBase.Stub(x => x.All<Job>()).Return(new InMemoryDbSet<Job> {older, old, @new});

            var handler = new LatestJobStatusQueryHandler(dataBase, mock);

            handler.Handle(new LatestJobStatusQuery());

            mock.AssertWasCalled(x => x.Handle(Arg<JobStatusQuery>.Matches(y => y.JobId == 3)));
        }
    }

    public class StubJob : Job
    {
        public StubJob(DateTime started) : base("foo")
        {
            Started = started;
        }
    }
}