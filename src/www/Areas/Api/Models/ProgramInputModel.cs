using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Areas.Api.Models
{
    public class ProgramInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name="Job Description Url")]
        public string JobDescriptionUrl { get; set; }
        
        [Display(Name="Default Max Runs")]
        public int? DefaultMaxRuns { get; set; }
    }
}