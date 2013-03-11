using NavigationRoutes;
using nTestSwarm.Areas.Client.Controllers;
using nTestSwarm.Areas.Diagnostics.Controllers;
using nTestSwarm.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace nTestSwarm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHubs();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("images/*");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "nTestSwarm.Controllers" }
            );

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            routes.MapNavigationRoute<JobController>("Jobs", c => c.Latest())
                .AddChildRoute<JobController>("Latest", c => c.Latest())
                .AddChildRoute<ProgramController>("Queue New", c => c.QueueJob((long?)null), "api");

            routes.MapNavigationRoute<RunController>("Join the Swarm", c => c.Index());

            routes.MapNavigationRoute<RunDiagnosticsController>("Diagnostics", c => c.Nullo())
                .AddChildRoute<RunDiagnosticsController>("Runs", c => c.Index(), "utils")
                .AddChildRoute<ClientDetectionController>("Client", c => c.Index(), "utils");
        }
    }
}