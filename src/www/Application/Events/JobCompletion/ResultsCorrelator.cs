using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public interface IResultsCorrelator
    {
        IEnumerable<RunUserAgentCompareResult> GetForJobs(Job source, Job target);
    }
    
    public class ResultsCorrelator : IResultsCorrelator
    {
        readonly IDataBase _db;
        readonly IResultsJoiner _joiner;

        public ResultsCorrelator(IDataBase db, IResultsJoiner joiner)
        {
            _db = db;
            _joiner = joiner;
        }

        public IEnumerable<RunUserAgentCompareResult> GetForJobs(Job source, Job target)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (target == null) throw new ArgumentNullException("target");

            var query = from rua in _db.All<RunUserAgent>().AsNoTracking()
                        where new[] {source.Id, target.Id}.Contains(rua.Run.JobId)
                        let result = new RunResult
                        {
                            RunName = rua.Run.Name,
                            RunId = rua.Run.Id,
                            RunUserAgentResult = rua.Result,
                            UserAgentName = rua.UserAgent.Name,
                            Url = rua.Run.Url
                        }
                        select new
                        {
                            rua.Run.JobId,
                            result
                        };
            
            var results = query.ToArray();
            var sourceResults = results.Where(x => x.JobId == source.Id).Select(x => x.result);
            var targetResults = results.Where(x => x.JobId == target.Id).Select(x => x.result);
            var joined = _joiner.Join(sourceResults, targetResults);

            return MapToResults(source, target, joined ?? Enumerable.Empty<CorrelatedResult>());
        }


        IEnumerable<RunUserAgentCompareResult> MapToResults(Job source, Job target, IEnumerable<CorrelatedResult> joined)
        {
            return joined.Select(result =>
            {
                var runStatusTransition = RunStatusTransition.GetTransition(result.Source.RunUserAgentResult, result.Target.RunUserAgentResult);

                return new RunUserAgentCompareResult
                {
                    RunName = result.Target.RunName,
                    SourceJobId = source.Id,
                    SourceJobName = source.Name,
                    TargetJobName = target.Name,
                    TargetJobId = target.Id,
                    UserAgentName = result.Target.UserAgentName,
                    TargetRunUrl = result.Target.Url,
                    ClientId = result.Target.RunUserAgentResult.ClientId,
                    RunId = result.Target.RunId,
                    Transition = runStatusTransition,
                };
            });
        }
    }

    public class JobDiffQuery : IRequest<IEnumerable<RunUserAgentCompareResult>>
    {
        public long TargetJobId { get; set; }
        public long SourceJobId { get; set; }
    }

    public class JobDiffQueryHandler : IHandler<JobDiffQuery, IEnumerable<RunUserAgentCompareResult>>
    {
        readonly IDataBase _db;
        readonly IResultsCorrelator _correlator;

        public JobDiffQueryHandler(IDataBase db, IResultsCorrelator correlator)
        {
            _db = db;
            _correlator = correlator;
        }

        public IEnumerable<RunUserAgentCompareResult> Handle(JobDiffQuery request)
        {
            var target = _db.Find<Job>(request.TargetJobId);
            var source = _db.Find<Job>(request.SourceJobId);
            
            return _correlator.GetForJobs(source, target);
        }
    }
}