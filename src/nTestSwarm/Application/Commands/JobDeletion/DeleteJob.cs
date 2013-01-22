namespace nTestSwarm.Application.Commands.JobDeletion
{
    public class DeleteJob
    {
        public DeleteJob(long jobId)
        {
            JobId = jobId;
        }

        public long JobId { get; set; }
    }
}