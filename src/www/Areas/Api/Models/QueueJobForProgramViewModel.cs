using System.Collections.Generic;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Api.Models
{
    public class QueueJobForProgramViewModel
    {

        public IEnumerable<SelectListItem> Programs { get; set; }

        public long ProgramId { get; set; }

    }
}