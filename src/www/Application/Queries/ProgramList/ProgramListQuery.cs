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

        private readonly Database _db;

        public ProgramListQueryHandler(Database db)
        {
            _db = db;
        }

        public IEnumerable<ProgramListResult> Handle(ProgramListQuery request)
        {
            const string sql = "SELECT " +
                                    "p.Id, p.Name, Count(j.Id) JobCount " +
                               "FROM " +
                                    "Programs p " +
                                    "LEFT JOIN Jobs j ON p.Id = j.Program_Id " +
                               "GROUP BY " +
                                    "p.Id, p.Name " +
                               "ORDER BY  " +
                                    "Name";

            return _db.SqlQuery<ProgramListResult>(sql).ToArray();
        }

    }
}