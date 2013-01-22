using System.Collections.Generic;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class BrowserComparer : IEqualityComparer<ResultDto>
    {
        public bool Equals(ResultDto x, ResultDto y)
        {
            return Equals(x.UserAgentId, y.UserAgentId);
        }

        public int GetHashCode(ResultDto obj)
        {
            return obj.UserAgentId.GetHashCode();
        }
    }
}