using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Areas.Api.Models
{
    public class CreateJobInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SuiteId { get; set; }
        public IList<CreateJobRunInput> Runs { get; set; }
    }

    public class CreateJobRunInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}