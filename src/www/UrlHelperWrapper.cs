using System.Web;
using System.Web.Routing;

namespace nTestSwarm
{
    public interface IUrlHelperWrapper
    {
        string Action(string actionName, string controllerName, object routeValues);
    }

    public class UrlHelperWrapper : IUrlHelperWrapper
    {
        private readonly System.Web.Mvc.UrlHelper _urlHelper;

        public UrlHelperWrapper ()
	    {
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(context);
            var request = new RequestContext(context, routeData);

            _urlHelper = new System.Web.Mvc.UrlHelper(request);
	    }

        public string Action(string actionName, string controllerName, object routeValues)
        {
            return _urlHelper.Action(actionName, controllerName, routeValues);
        }        
    }
}