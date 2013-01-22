namespace nTestSwarm.Application.Events.JobCompletion
{
    public class CorrelatedResult
    {
        public CorrelatedResult(RunResult source, RunResult target)
        {
            Source = source;
            Target = target;
        }

        public RunResult Source { get; set; }
        public RunResult Target { get; set; }

        public override string ToString()
        {
            return string.Format("Source: {0} | Target: {1}", Source, Target);
        }
    }
}