using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.ProgramList
{
    public class ProgramListQuery : IRequest<IEnumerable<ProgramListResult>>
    {
        public long? ProgramId { get; set; }
    }

    public class ProgramListResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int JobCount { get; set; }
        public string LastJobStatus { get; set; }
    }

    public class ProgramListQueryHandler : IHandler<ProgramListQuery, IEnumerable<ProgramListResult>>
    {

        private readonly IDataBase _db;

        public ProgramListQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<ProgramListResult> Handle(ProgramListQuery request)
        {
            return (from program in _db.All<Program>().AsNoTracking()
                    orderby program.Name
                    select new ProgramListResult
                    {
                        Id = program.Id,
                        Name = program.Name,
                        //TODO: optimize and figure out how to get status
                        JobCount = program.Jobs.Count()
                    }).ToArray();
        }

    }
}