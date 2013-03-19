using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class RunDto
    {
        public RunDto(ResultDto key, IEnumerable<ResultDto> cells)
        {
            RunName = key.RunName;
            RunId = key.RunId;
            RunUrl = key.RunUrl;

            Cells = cells.Select(x => new ResultCellDto(x)).ToArray();
        }

        public string RunName { get; set; }
        public IEnumerable<ResultCellDto> Cells { get; set; }
        public long RunId { get; set; }
        public string RunUrl { get; set; }
    }
}