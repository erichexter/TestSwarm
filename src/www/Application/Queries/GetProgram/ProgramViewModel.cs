using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [Display(Name = "JobId Description Url")]
        public string JobDescriptionUrl { get; set; }

        [Display(Name = "Default Max Runs")]
        public int? DefaultMaxRuns { get; set; }

        public IEnumerable<Descriptor> UserAgents 
        {   
            set 
            {
                UserAgentListItems = value.Select(ua => new SelectListItem { Value = ua.Id.ToString(), Text = ua.Name, Selected = ua.Selected });
            } 
        }

        [Display(Name= "User Agents")]
        public IEnumerable<SelectListItem> UserAgentListItems { get; set; }
    }
}