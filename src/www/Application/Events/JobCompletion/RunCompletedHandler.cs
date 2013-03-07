﻿using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Hubs;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class RunCompletedHandler : IHandler<RunCompleted>
    {
        private readonly IBus _bus;

        public RunCompletedHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(RunCompleted message)
        {
            var status = _bus.Request(new JobStatusQuery(message.JobId)).Data;
            
            if (status != null)
                JobStatusHub.UpdateStatus(status);
        }
    }
}
