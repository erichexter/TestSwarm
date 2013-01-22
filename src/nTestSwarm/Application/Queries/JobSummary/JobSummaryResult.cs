using System;

namespace nTestSwarm.Application.Queries.JobSummary
{
    public class JobSummaryResult
    {
        public int TotalRuns { get; set; }
        public int TimeoutRuns { get; set; }
        public int ErrorRuns { get; set; }
        public int FailRuns { get; set; }
        public int InProgressRuns { get; set; }
        public int NotStartedRuns { get; set; }
        public int PassRuns { get; set; }

        public int TotalTests { get; set; }
        public int TotalFailedTests { get; set; }
        
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }

        public int GetPassingTestCount()
        {
            return TotalTests - TotalFailedTests;
        }

        public int GetPassingTestPercentOfTotal()
        {
            return GetPassingTestCount().AsPercentageOf(TotalTests);
        }

        public int GetPassingRunsPercentOfTotalRuns()
        {
            return PassRuns.AsPercentageOf(TotalRuns);
        }

        public int GetExecutingRunsPercentofTotalRuns()
        {
            return (PassRuns + FailRuns).AsPercentageOf(TotalRuns);
        }
    }

    public static class PercentExtension
    {
        public static int AsPercentageOf(this int part, int total)
        {
            if (part == 0 || total == 0) return 0;
            
            decimal percent = ((decimal)part / total) * 100;

            return (int)Math.Round(percent, 0, MidpointRounding.AwayFromZero);
        }
    }


}