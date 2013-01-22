using System.Collections.Generic;
using nTestSwarm.Application.Domain;
using nTestSwarm.Models;

namespace nTestSwarm.Areas.Admin.Models
{
    public class JobCopyModel
    {
        public Job JobToCopy { get; set; }
        public IEnumerable<Run> RunsForJob { get; set; }
        public JobCopyInputModel Defaults { get; set; }
    }

    public class JobCopyInputModel
    {
        public string JobNameFormat { get; set; }
        public long JobId { get; set; }
        public string Correlation { get; set; }
    }
}