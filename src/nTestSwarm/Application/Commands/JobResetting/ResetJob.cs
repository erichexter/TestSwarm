namespace nTestSwarm.Application.Commands.JobResetting
{
    public class ResetJob
    {
        public ResetJob(long jobId)
        {
            JobId = jobId;
        }

        public long JobId { get; set; }
    }
}