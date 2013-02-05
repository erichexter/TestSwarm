using System.Linq;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.JobCreation.Described
{
    public class CreateJobFromDescriptionHandler : IHandler<CreateJobFromDescription, CreateJobResult>
    {
        readonly IBus _bus;
        readonly IJobDescriptionClient _descriptionClient;

        public CreateJobFromDescriptionHandler(IBus bus, IJobDescriptionClient descriptionClient)
        {
            _bus = bus;
            _descriptionClient = descriptionClient;
        }

        public CreateJobResult Handle(CreateJobFromDescription request)
        {
            var desc = _descriptionClient.GetFrom(request.Url, request.Correlation);

            var createJobMessage = new CreateJob
            {
                Name = desc.Name,
                Runs = desc.Runs.Select(x => new CreateJob.CreateNewRun {Name = x.Name, Url = x.Url}),
                SuiteId = desc.SuiteId,
            };

            if(request.MaxRuns.HasValue)
            {
                createJobMessage.MaxRuns = request.MaxRuns;
            }

            var result = _bus.Request(createJobMessage);

            return result.Data;
        }
    }
}