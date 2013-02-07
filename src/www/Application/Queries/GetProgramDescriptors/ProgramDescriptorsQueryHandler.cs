using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.GetProgramDescriptors
{
    public class ProgramDescriptorsQueryHandler : IHandler<ProgramDescriptorsQuery,IEnumerable<Descriptor>>
    {
        private readonly IDataBase _db;

        public ProgramDescriptorsQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<Descriptor> Handle(ProgramDescriptorsQuery request)
        {
            return (from p in _db.All<Program>().AsNoTracking()
                    orderby p.Name
                    select new Descriptor { Id = p.Id, Name = p.Name })
                    .ToArray();
        }
    }
}