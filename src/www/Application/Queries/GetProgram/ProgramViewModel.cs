using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace nTestSwarm.Application.Queries.GetProgram
{
    public class ProgramViewModel
    {
        public long ProgramId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Job Description Url")]
        public string JobDescriptionUrl { get; set; }

        [Display(Name = "Default Max Runs")]
        public int? DefaultMaxRuns { get; set; }
        
        [Display(Name= "User Agents")]
        public SelectListItem[] UserAgents { get; set; }
    }
}