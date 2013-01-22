using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public interface IResultsJoiner
    {
        IEnumerable<CorrelatedResult> Join(IEnumerable<RunResult> sourceResults, IEnumerable<RunResult> targetResults);
    }

    public class ResultsJoiner : IResultsJoiner
    {
        public IEnumerable<CorrelatedResult> Join(IEnumerable<RunResult> sourceResults,
                                                  IEnumerable<RunResult> targetResults)
        {
            var sourceArray = (sourceResults ?? Enumerable.Empty<RunResult>()).ToArray();
            var targetArray = (targetResults ?? Enumerable.Empty<RunResult>()).ToArray();

            if (targetArray.IsEmpty())
                return Enumerable.Empty<CorrelatedResult>();

            if (sourceResults.IsEmpty())
                return targetArray.Select(x => new CorrelatedResult(new NullResult(), x));

            return joinTwoNonEmptyCollections(sourceArray, targetArray);
        }

        IEnumerable<CorrelatedResult> joinTwoNonEmptyCollections(RunResult[] sourceResults, RunResult[] targetResults)
        {
            return from targetResult in (targetResults ?? Enumerable.Empty<RunResult>())
                   let matchingSourceResult = (sourceResults ?? Enumerable.Empty<RunResult>()).SingleOrDefault(x => x.RunName == targetResult.RunName && x.UserAgentName == targetResult.UserAgentName) ?? new NullResult()
                   select new CorrelatedResult(matchingSourceResult, targetResult);
        }
    }
}