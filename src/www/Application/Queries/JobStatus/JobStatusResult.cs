using System;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class JobStatusResult
    {
        public JobStatusResult()
        {
        }

        public JobStatusResult(IEnumerable<ResultDto> results)
        {
            if (results == null)
                throw new ArgumentNullException("results");
            
            var array = results.ToArray();

            Browsers = (from browser in OrderByBrowser(array).Distinct(new BrowserComparer())
                        select new UserAgentDto(browser))
                        .ToArray();

            RunResults = (from result in array
                          orderby result.RunName , result.RunUrl , result.RunId
                          group result by result.RunId
                          into grouping
                          select new RunDto(grouping.First(), OrderByBrowser(grouping)))
                          .ToArray();

            JobId = array.First().JobId;
            JobName = array.First().JobName;
        }

        public bool IsEmpty { get { return JobName == null; } }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public IEnumerable<UserAgentDto> Browsers { get; set; }
        public IEnumerable<RunDto> RunResults { get; set; }

        IEnumerable<ResultDto> OrderByBrowser(IEnumerable<ResultDto> results)
        {
            return from r in results
                   orderby r.UserAgentBrowser , r.UserAgentName , r.UserAgentId
                   select r;
        }
    }
}