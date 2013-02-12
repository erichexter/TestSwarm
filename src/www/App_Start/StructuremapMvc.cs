using Microsoft.AspNet.SignalR;
using StructureMap;
using System.Web.Mvc;
using nTestSwarm.DependencyResolution;


[assembly: WebActivator.PreApplicationStartMethod(typeof(nTestSwarm.App_Start.StructuremapMvc), "Start")]

namespace nTestSwarm.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            var resolver = new SmDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
            GlobalHost.DependencyResolver = new SignalRDependencyResolver(container);
        }
    }
}