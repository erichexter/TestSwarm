using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Queries.AvailableJobDiff;

namespace nTestSwarmTests.Application.AvailableJobDiffs
{
    public class AvailableJobDiffsQueryHandlerTests : IntegrationTestBase
    {
        [Test]
        public void Should_get_available_job_diffs()
        {
            var r1 = new RunUserAgentCompareResult {SourceJobId = 1, TargetJobId = 2, SourceJobName = "source1", TargetJobName = "target2", RunName = "run1"};
            var r2 = new RunUserAgentCompareResult {SourceJobId = 1, TargetJobId = 2, SourceJobName = "source1", TargetJobName = "target2", RunName = "run2"};
            var r3 = new RunUserAgentCompareResult {SourceJobId = 1, TargetJobId = 3, SourceJobName = "source1", TargetJobName = "target3", RunName = "run1"};
            var r4 = new RunUserAgentCompareResult {SourceJobId = 1, TargetJobId = 3, SourceJobName = "source1", TargetJobName = "target3", RunName = "run2"};
            var r5 = new RunUserAgentCompareResult {SourceJobId = 2, TargetJobId = 3, SourceJobName = "source2", TargetJobName = "target3", RunName = "run1"};
            var r6 = new RunUserAgentCompareResult {SourceJobId = 2, TargetJobId = 3, SourceJobName = "source2", TargetJobName = "target3", RunName = "run2"};

            Save(r1, r2, r3, r4, r5, r6);

            IEnumerable<AvailableJobDiffsResult> result = null;

            WithDbContext(context =>
            {
                result = GetInstance<AvailableJobDiffsQuerier>().Handle(new AvailableJobDiffQuery());
                result.Count().ShouldEqual(3);

                result.Single(x => x.SourceJobId == 1 && x.TargetJobId == 2).Name.ShouldEqual("source1 to target2");
                result.Single(x => x.SourceJobId == 2 && x.TargetJobId == 3).Name.ShouldEqual("source2 to target3");
                result.Single(x => x.SourceJobId == 1 && x.TargetJobId == 3).Name.ShouldEqual("source1 to target3");

            });

        }
    }
}