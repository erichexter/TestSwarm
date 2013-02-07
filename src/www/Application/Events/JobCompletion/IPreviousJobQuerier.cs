using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public interface IPreviousJobQuerier
    {
        Job GetPreviousCompleteJob(Job job);
    }

    public class PreviousJobQuerier : IPreviousJobQuerier
    {
        readonly IDataBase _db;

        public PreviousJobQuerier(IDataBase db)
        {
            _db = db;
        }

        public Job GetPreviousCompleteJob(Job job)
        {
            // this assumes one job suite for the entire system
            // TODO incorporate SuiteId
            return (from jobs in _db.All<Job>()
                    where jobs.Status == JobStatusType.Complete && jobs.Finished < job.Finished
                    orderby jobs.Finished descending 
                    select jobs).Take(1).SingleOrDefault();
        }
    }
}