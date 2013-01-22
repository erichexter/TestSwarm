using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.JobDeletion
{
    public class DeleteJobHandler : IHandler<DeleteJob>
    {
        readonly IDataBase _db;

        public DeleteJobHandler(IDataBase db)
        {
            _db = db;
        }

        public void Handle(DeleteJob message)
        {
            var runs_for_job = _db.All<Run>().Where(x => x.Job.Id == message.JobId);
            var runIds = runs_for_job.Select(r => r.Id).ToArray();
            var client_runs = _db.All<ClientRun>().Where(x => runIds.Contains(x.Run.Id));
            var run_user_agents = _db.All<RunUserAgent>().Where(x => runIds.Contains(x.Run.Id));

            foreach (var cr in client_runs) _db.Remove(cr);

            foreach (var rua in run_user_agents) _db.Remove(rua);

            foreach (var run in runs_for_job) _db.Remove(run);

            _db.Remove<Job>(message.JobId);
            _db.SaveChanges();
        }
    }
}