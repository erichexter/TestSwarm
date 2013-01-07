using NUnit.Framework;
using Should;
using nTestSwarm.Application;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;

namespace nTestSwarmTests.StructureMap
{
    [TestFixture]
    public class StructureMapTests : IntegrationTestBase
    {
        [Test]
        public void Should_resolve_a_handler_by_the_request_and_respose_types()
        {
            var requestedType = typeof (IHandler<JobStatusQuery, JobStatusResult>);

            var instance = GetInstance(requestedType);

            instance.ShouldBeType<JobStatusQueryHandler>();
        }

        [Test]
        public void Should_resolve_handler_for_domain_events()
        {
            
        }
    }
}