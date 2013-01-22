using System.Linq;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.RunResetting
{
    public class ResetRunHandler : IHandler<ResetRun>
    {
        readonly nTestSwarmContext _db;
        readonly IOutputCacheInvalidator _invalidator;

        public ResetRunHandler(nTestSwarmContext db, IOutputCacheInvalidator invalidator)
        {
            _db = db;
            _invalidator = invalidator;
        }

        public void Handle(ResetRun message)
        {
            long userAgent = (from client in _db.Clients
                             where client.Id == message.ClientId
                             select client.UserAgentId).FirstOrDefault();

            if (userAgent != default(long))
            {
                var runUserAgents = (from r in _db.AllIncluding<RunUserAgent>(x => x.Run.Job)
                                     where r.UserAgentId == userAgent &&
                                           r.RunId == message.RunId
                                     select r);

                var rua = runUserAgents.FirstOrDefault();

                if (rua != null)
                {
                    var runClientsToDelete = from clientRun in _db.ClientRuns
                                             where clientRun.RunId == message.RunId
                                                   && clientRun.ClientId == message.ClientId
                                                   && clientRun.Client.UserAgentId == userAgent
                                             select clientRun;

                    runClientsToDelete.Each(x => _db.Remove(x));

                    rua.Reset();
                    rua.Run.ContinueRun();
                    _db.SaveChanges();
                    _invalidator.InvalidateJobStatus(rua.Run.Job.Id);
                }
            }
        }
    }
}