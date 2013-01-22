using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using System.Linq;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public interface IJobCompletedEventDistributor
    {
        void DistributeAccumlatedJobCompletedEvents();
    }

    public class JobCompletedEventDistributor : IJobCompletedEventDistributor
    {
        readonly IBus _bus;
        readonly IEventStore _store;

        public JobCompletedEventDistributor(IBus bus, IEventStore store)
        {
            _bus = bus;
            _store = store;
        }

        public void DistributeAccumlatedJobCompletedEvents()
        {
            using (var stream = _store.GetNotProcessedStream())
            {
                var needAttention = stream.All().OfType<JobCompleted>();

                foreach (var jobCompleted in needAttention)
                {
                    _bus.Send(jobCompleted);

                    stream.MarkAsProcessed(jobCompleted);
                }
            }
        }
    }
}