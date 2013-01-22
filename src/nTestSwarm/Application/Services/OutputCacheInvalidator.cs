using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nTestSwarm.Application.Services
{
    public interface IOutputCacheInvalidator
    {
        void InvalidateJobStatus(long jobId);
    }

    public class OutputCacheInvalidator : IOutputCacheInvalidator
    {
        readonly HttpContextBase _context;
        readonly UrlHelper _url;

        public OutputCacheInvalidator(HttpContextBase context)
        {
            _context = context;
            _url = new UrlHelper(new RequestContext(_context, new RouteData()));
        }

        public void InvalidateJobStatus(long jobId)
        {
            var index = _url.Action("Index", "Job", new { id = jobId });
            var statusTable = _url.Action("StatusTable", "Job", new { id = jobId });

            _context.Response.RemoveOutputCacheItem(index);
            _context.Response.RemoveOutputCacheItem(statusTable);
        }
    }
}