//using System.Collections.Generic;
//using System.Linq;
//using nTestSwarm.Application.Domain;
//using nTestSwarm.Application.Infrastructure.BusInfrastructure;
//using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.JobCreation.Copy
{
    //public class JobCopier : IHandler<CopyJob, CreateJobResult>
    //{
    //    readonly IBus _bus;
    //    readonly IDataBase _db;

    //    public JobCopier(IBus bus, IDataBase db)
    //    {
    //        _bus = bus;
    //        _db = db;
    //    }

    //    public CreateJobResult Handle(CopyJob request)
    //    {
    //        var existingJob = _db.Find<JobId>(request.JobId);

    //        var createNewJob = new CreateJob
    //            {
    //                Name = GetName(request),
    //                Runs = GetRuns(existingJob)
    //            };

    //        var result = _bus.Request(createNewJob);

    //        return result.Data;
    //    }

    //    static IEnumerable<CreateJob.CreateNewRun> GetRuns(JobId existingJob)
    //    {
    //        return existingJob.Runs.Select(x => new CreateJob.CreateNewRun {Name = x.Name, Url = x.Url});
    //    }

    //    static string GetName(CopyJob request)
    //    {
    //        return string.Format(request.JobNameFormat ?? string.Empty, request.JobNameParams ?? new[] {string.Empty});
    //    }
    //}
}