using Microsoft.AspNet.SignalR;
using StructureMap;
using System.Web.Mvc;
using www.DependencyResolution;


[assembly: WebActivator.PreApplicationStartMethod(typeof(www.App_Start.StructuremapMvc), "Start")]

namespace www.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            var resolver = new SmDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
            GlobalHost.DependencyResolver = new SignalRDependencyResolver(container);
        }
    }
}