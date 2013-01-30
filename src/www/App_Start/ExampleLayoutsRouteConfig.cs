using NavigationRoutes;
using nTestSwarm.Areas.Utils.Controllers;
using nTestSwarm.Controllers;
using System.Web.Routing;
using ApiJobController = nTestSwarm.Areas.Api.Controllers.JobController;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            routes.MapNavigationRoute<JobController>("Jobs", c => c.Index(0))
                .AddChildRoute<JobController>("Latest", c => c.Latest())
                .AddChildRoute<ApiJobController>("Queue New", c => c.DescribeNew("", new string[] {}, null),"api");

            routes.MapNavigationRoute<RunController>("Join the Swarm", c => c.Index());

            routes.MapNavigationRoute<RunDiagnosticsController>("Diagnostics", c => c.Nullo())
                .AddChildRoute<RunDiagnosticsController>("Runs", c => c.Index(),"utils")
                .AddChildRoute<ClientDetectionController>("Client", c => c.Index(), "utils");
        }
    }
}
