using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Models
{
    public class CreateJobRunInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}