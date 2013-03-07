using System.Web.Http;

namespace nTestSwarm
{
    public static class WebApiConfig
    {
        public const string DefaultRoute = "DefaultApi";

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: DefaultRoute,
                routeTemplate: "api/{controller}/{id}/{action}"
            );
        }
    }
}
