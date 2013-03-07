using nTestSwarm.Filters;
using System.Web.Http;

namespace nTestSwarm
{
    public static class WebApiConfig
    {
        public const string DefaultRoute = "DefaultApi";

        public static void Register(HttpConfiguration config)
        {
            config.ConfigureRoutes();
            config.ConfigureFilters();
        }

        private static void ConfigureRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: DefaultRoute,
                routeTemplate: "api/{controller}/{id}/{action}"
            );
        }

        private static void ConfigureFilters(this HttpConfiguration config)
        {
            config.Filters.Add(new ValidateAttribute());
        }
    }
}
