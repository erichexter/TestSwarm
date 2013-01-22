using System.Web.Mvc;
using StructureMap;
using nTestSwarm.Application.Infrastructure.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(nTestSwarm.App_Start.StructuremapMvc), "Start")]

namespace nTestSwarm.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}