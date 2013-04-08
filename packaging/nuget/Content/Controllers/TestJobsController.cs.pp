using System.Web;
using System.Web.Http;
using System.Web.Routing;
using $rootnamespace$.Models;

namespace $rootnamespace$.Controllers
{
    public class TestJobsController : ApiController
    {
        public JobDescriptor Get()
        {
            var urlHelper = GetUrlHelper();
            var job = new JobDescriptor();
            
            job.AddRun("Sample Test Suite 1", Request.Headers.Host + urlHelper.Content("~/Tests/Test1.html"));
            job.AddRun("Sample Test Suite 2", Request.Headers.Host + urlHelper.Content("~/Tests/Test2.html"));
            
            return job;
        }

        private static System.Web.Mvc.UrlHelper GetUrlHelper()
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(context);
            var request = new System.Web.Routing.RequestContext(context, routeData);
            var helper = new System.Web.Mvc.UrlHelper(request);

            return helper;
        }
    }
}