using NavigationRoutes;
using nTestSwarm.Areas.Client.Controllers;
using nTestSwarm.Areas.Diagnostics.Controllers;
using nTestSwarm.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using ClientRunController = nTestSwarm.Areas.Client.Controllers.RunController;

namespace nTestSwarm
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHubs();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("images/*");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { typeof(HomeController).Namespace }
            );

            routes.MapNavigationRoutes();
        }

        private static void MapNavigationRoutes(this RouteCollection routes)
        {
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            routes.MapNavigationRoute<JobController>("Jobs", c => c.Nullo())
                .AddChildRoute<JobController>("Latest", c => c.Latest())
                .AddChildRoute<ProgramController>("Queue New", c => c.QueueJob((long?)null));

            routes.MapNavigationRoute<ClientRunController>("Join the Swarm", c => c.Index(), "Client");

            routes.MapNavigationRoute<RunDiagnosticsController>("Diagnostics", c => c.Nullo())
                .AddChildRoute<RunDiagnosticsController>("Runs", c => c.Index(), "Diagnostics")
                .AddChildRoute<ClientDetectionController>("Client", c => c.Index(), "Diagnostics");
        }
    }
}