using System.Collections.Generic;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class JobResultsDiffer : IHandler<JobCompleted>
    {
        readonly IDataBase _db;
        readonly IPreviousJobQuerier _previousJobQuerier;
        readonly IResultsCorrelator _resultsCorrelator;

        public JobResultsDiffer(IPreviousJobQuerier previousJobQuerier,
                                IResultsCorrelator resultsCorrelator,
                                IDataBase db)
        {
            _previousJobQuerier = previousJobQuerier;
            _resultsCorrelator = resultsCorrelator;
            _db = db;
        }

        public void Handle(JobCompleted message)
        {
            var target = _db.Find<Job>(message.Job);

            var source = _previousJobQuerier.GetPreviousCompleteJob(target);

            if (source == null) return;

            IEnumerable<RunUserAgentCompareResult> compareResults = _resultsCorrelator.GetForJobs(source, target);
            
            compareResults.Each(_db.Add);

            _db.SaveChanges();
        }
    }
}