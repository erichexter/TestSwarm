using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace www
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHubs();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("images/*");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("WipeJob", "job/wipejob", new { controller = "Job", action = "WipeJob" }, new[] { "nTestSwarm.Controllers" });
            routes.MapRoute("Latest", "job/latest", new { controller = "Job", action = "Latest" }, new[] { "nTestSwarm.Controllers" });
            routes.MapRoute("JobStatus", "job/{id}", new { controller = "Job", action = "Index" }, new[] { "nTestSwarm.Controllers" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "nTestSwarm.Controllers" }
                );

        }
    }
}