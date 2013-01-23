using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.CompletedRun
{
    public class CompleteRunHandler : IHandler<CompleteRun>
    {
        readonly IDataBase _db;

        public CompleteRunHandler(IDataBase db)
        {
            _db = db;
        }

        public void Handle(CompleteRun message)
        {
            var clientRun = _db.AllIncluding<ClientRun>(x => x.Client,
                                x => x.Client.UserAgent,
                                x => x.Run.Job,
                                x => x.Run.RunUserAgents.Select(r => r.UserAgent),
                                x => x.Run.ClientRuns)

                .FirstOrDefault(x => x.ClientId == message.Client_Id && x.RunId == message.Run_id);

            if (clientRun != null)
            {
                clientRun.Apply(message);
            }

            _db.SaveChanges();
        }
    }
}