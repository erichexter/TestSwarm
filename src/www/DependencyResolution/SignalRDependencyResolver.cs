using Microsoft.AspNet.SignalR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace www.DependencyResolution
{
    public class SignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IContainer _container;

        public SignalRDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            var service = _container.TryGetInstance(serviceType);

            if (service == null)
            {
                return base.GetService(serviceType);
            }
            else
            {
                return service;
            }
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var services = _container.GetAllInstances(serviceType);

            if (services == null || services.Count == 0)
            {
                return base.GetServices(serviceType);
            }
            else
            {
                return services.Cast<object>();
            }
        }
    }
}