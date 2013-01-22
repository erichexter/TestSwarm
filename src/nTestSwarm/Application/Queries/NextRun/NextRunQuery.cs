using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.NextRun
{
    public class NextRunQuery : IRequest<NextRunResult>
    {
        public NextRunQuery(long clientId)
        {
            ClientId = clientId;
        }

        public long ClientId { get; set; }
    }

    public class NextRunResult
    {
        public NextRunResult(Run nextRun)
        {
            id = nextRun.Id;
            url = nextRun.Url;
            desc = nextRun.Name;
        }

        public long id { get; set; }
        public string url { get; set; }
        public string desc { get; set; }
    }
}