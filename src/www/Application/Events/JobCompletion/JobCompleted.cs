using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using nTestSwarm.Hubs;

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
            var target = _db.Find<Job>(message.JobId);

            var source = _previousJobQuerier.GetPreviousCompleteJob(target);

            if (source == null) return;

            IEnumerable<RunUserAgentCompareResult> compareResults = _resultsCorrelator.GetForJobs(source, target);
            
            compareResults.Each(_db.Add);

            _db.SaveChanges();
        }
    }


    public class JobComleteHandlerNotifier : IHandler<JobCompleted>
    {

        public void Handle(JobCompleted message)
        {
            GlobalHost.ConnectionManager.GetHubContext<JobStatusHub>().Clients.All.finished(message.JobId);
        }
    }

}