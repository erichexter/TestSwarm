using Newtonsoft.Json.Serialization;
using nTestSwarm.Filters;
using System.Web.Http;

namespace nTestSwarm
{
    public static class WebApiConfig
    {
        public const string DefaultRoute = "DefaultApi";
        public const string NextRunRoute = "NextRun";

        public static void Register(HttpConfiguration config)
        {
            config.ConfigureRoutes();
            config.ConfigureFilters();
        }

        private static void ConfigureRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: DefaultRoute,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: NextRunRoute,
                routeTemplate: "api/clients/{id}/nextrun",
                defaults: new { controller = "Clients", action = "NextRun" }
            );
        }

        private static void ConfigureFilters(this HttpConfiguration config)
        {
            config.Filters.Add(new ApiValidateAttribute());
        }

        private static void ConfigureFormatters(this HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
