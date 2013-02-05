using System.Web.Mvc;

namespace nTestSwarm.Areas.Api.Models
{
    public class QueueJobForProgramViewModel
    {

        public SelectList Programs { get; set; }

        public int ProgramId { get; set; }

    }
}