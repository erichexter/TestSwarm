using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using nTestSwarm.Application;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using Configuration = nTestSwarm.Migrations.Configuration;

namespace nTestSwarm
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("images/*");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("WipeJob", "job/wipejob", new { controller = "Job", action = "WipeJob" }, new[] { "nTestSwarm.Controllers" });
            routes.MapRoute("Latest", "job/latest", new { controller = "Job", action = "Latest" }, new[] { "nTestSwarm.Controllers" });
            routes.MapRoute("JobStatus", "job/{id}", new { controller = "Job", action = "Index" }, new[] { "nTestSwarm.Controllers" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}, // Parameter defaults
                new[] {"nTestSwarm.Controllers"}
                );

            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            DomainEvents.ClearCallbacks();
        }
    }
}