using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace nTestSwarm.Application
{
    public static class PartialExtensions
    {
        public static MvcHtmlString LatestJobs(this HtmlHelper helper)
        {
            return helper.Partial("LatestJobs");
        }

        public static MvcHtmlString ClientDetection(this HtmlHelper helper)
        {
            return helper.Action("Index", "ClientDetection", new {area = "Utils"});
        }
    }
}