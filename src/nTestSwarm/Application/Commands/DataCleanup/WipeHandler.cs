using System.Linq;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.DataCleanup
{
    public class WipeHandler : IHandler<Wipe>
    {
        readonly nTestSwarmContext _db;
        readonly ISystemTime _time;

        public WipeHandler(nTestSwarmContext db, ISystemTime time)
        {
            _db = db;
            _time = time;
        }

        public void Handle(Wipe message)
        {
            var now = _time.Now;
            var fiveMinutesAgo = now.AddMinutes(-5);

            var clientRuns = from clientRun in _db.All<ClientRun>()
                             where (clientRun.Updated < fiveMinutesAgo)
                                   && clientRun.Status == (int) RunStatusType.Running
                             select clientRun;

            foreach (var clientRun in clientRuns)
            {
                var cr = clientRun;
                
                _db.Remove(cr);


            }

            // not sure if needed:

            var runUserAgentsToReset = from runUserAgent in _db.RunUserAgents
                                       where runUserAgent.RemainingRuns == 0
                                       select runUserAgent;

            runUserAgentsToReset = runUserAgentsToReset.Except(
                from runUserAgent in _db.RunUserAgents
                from clientRun in _db.ClientRuns
                from client in _db.Clients
                where clientRun.RunId == runUserAgent.RunId
                && clientRun.ClientId == client.Id
                && client.UserAgentId == runUserAgent.UserAgentId
                select runUserAgent);

            foreach (var runUserAgent in runUserAgentsToReset)
            {
                runUserAgent.Reset();
            }

            _db.SaveChanges();
        }
    }
}