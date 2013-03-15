﻿using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Models;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQuery : IRequest<JobDetailsViewModel>
    {
        public LatestJobForProgramQuery(int programId)
        {
            ProgramId = programId;
        }

        public long ProgramId { get; private set; }
    }
}