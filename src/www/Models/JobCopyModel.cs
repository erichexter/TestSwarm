using nTestSwarm.Application.Domain;
using System.Collections.Generic;

namespace nTestSwarm.Models
{
    public class JobCopyModel
    {
        public Job JobToCopy { get; set; }
        public IEnumerable<Run> RunsForJob { get; set; }
        public JobCopyInputModel Defaults { get; set; }
    }
}