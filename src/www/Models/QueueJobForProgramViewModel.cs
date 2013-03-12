using nTestSwarm.Application.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace nTestSwarm.Models
{
    public class QueueJobForProgramViewModel
    {
        public QueueJobForProgramViewModel(long programId, IEnumerable<Descriptor> programDescriptors)
        {
            var programs = programDescriptors.Select(x => new SelectListItem 
                                                            { 
                                                                Value = x.Id.ToString(), 
                                                                Text = x.Name, 
                                                                Selected = x.Id == programId
                                                            }).ToList();
                            

            programs.Insert(0, new SelectListItem { Text = "(Select)" });

            Programs = programs;
        }

        public IEnumerable<SelectListItem> Programs { get; private set; }

        public long ProgramId { get; private set; }
    }
}