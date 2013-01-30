using nTestSwarm.Application.Queries.ProgramList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nTestSwarm.Areas.Api.Models
{
    public class ProgramsViewModel
    {
        public IEnumerable<ProgramListResult> Programs { get; set; }

    }
}