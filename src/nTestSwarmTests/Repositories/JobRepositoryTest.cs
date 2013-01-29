using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;
using System.Diagnostics;

namespace nTestSwarmTests.Repositories
{
    [TestFixture]
    public class JobRepositoryTest : IntegrationTestBase
    {
        [Test]
        public void Should_Save_a_Job()
        {
            var fixture = new Fixture();
            var job = fixture.Build<Job>()
                .Without(c => c.Id)
                .CreateAnonymous();
            using (var saveContext = DbContext())
            {
                var repo = new Repository<Job>(saveContext);
                repo.InsertOrUpdate(job);
                repo.Save();
            }


            Job savedJob;
            using (var readContext = DbContext())
            {
                var repo1 = new Repository<Job>(readContext);
                savedJob = repo1.Find(job.Id);
            }
            //            savedJob.Id.ShouldEqual(1);
            var compare = new KellermanSoftware.CompareNetObjects.CompareObjects();

            // savedJob is a proxy
            compare.MaxDifferences = 1;

            compare.Compare(job, savedJob);
            (compare.Differences.Count <= 1).ShouldBeTrue();

            Debug.WriteLine(compare.DifferencesString);
        }
    }
}