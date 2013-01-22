using NUnit.Framework;
using Should;
using nTestSwarm.Application.Queries.JobSummary;

namespace nTestSwarmTests.Application
{
    [TestFixture]
    public class PercentExtensionTests
    {
        [Test]
        public void Percent()
        {
            5.AsPercentageOf(10).ShouldEqual(50);
            3.AsPercentageOf(9).ShouldEqual(33);
            3.AsPercentageOf(0).ShouldEqual(0);
            0.AsPercentageOf(9).ShouldEqual(0);
        }
    }
}