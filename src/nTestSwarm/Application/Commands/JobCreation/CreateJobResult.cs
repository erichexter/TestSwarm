using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Commands.JobCreation
{
    public class CreateJobResult
    {
        public CreateJobResult(Job job)
        {
            JobId = job.Id;
        }

        public long JobId { get; set; }

        public string GetStatusUrl()
        {
            return string.Format("/job/{0}", JobId);
        }
    }
}