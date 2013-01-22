using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.JobResetting
{
    public class ResetJobHandler : IHandler<ResetJob>
    {
        readonly IDataBase _db;
        readonly IOutputCacheInvalidator _invalidator;

        public ResetJobHandler(IDataBase db, IOutputCacheInvalidator invalidator)
        {
            _db = db;
            _invalidator = invalidator;
        }

        public void Handle(ResetJob message)
        {
            var foundJob =
                _db.AllIncluding<Job>(x => x.Runs.Select(r => r.RunUserAgents),
                                      x => x.Runs.Select(r => r.ClientRuns))
                    .Where(j => j.Id == message.JobId);

            var job = foundJob.FirstOrDefault();

            if (job != null)
            {
                job.Runs.Each(run =>
                    {
                        var clientRuns = run.ClientRuns.ToArray();
                        clientRuns.Each(x =>
                            {
                                _db.Remove(x);
                                run.ClientRuns.Remove(x);
                            });
                        RunUserAgent[] runUserAgents = run.RunUserAgents.ToArray();
                        runUserAgents.Each(x => x.Reset());
                        run.Reset();
                    });

                job.Reset();

                _db.SaveChanges();
                _invalidator.InvalidateJobStatus(job.Id);
            }
        }
    }
}