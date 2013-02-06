using nTestSwarm.Application.Queries.ProgramList;
using System.Collections.Generic;

namespace nTestSwarm.Areas.Api.Models
{
    public class ProgramsViewModel
    {
        public IEnumerable<ProgramListResult> Programs { get; set; }
    }
}