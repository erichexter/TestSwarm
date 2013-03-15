using Newtonsoft.Json.Serialization;
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
            config.ConfigureFormatters();
            config.EnableSystemDiagnosticsTracing();
        }

        private static void ConfigureRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: DefaultRoute,
                routeTemplate: "api/{controller}/{id}/{action}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
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
