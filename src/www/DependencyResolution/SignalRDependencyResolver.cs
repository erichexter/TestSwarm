using Microsoft.AspNet.SignalR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.DependencyResolution
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
            if (_container.Model.HasDefaultImplementationFor(serviceType))
            {
                return _container.GetInstance(serviceType);
            }
            else 
            {
                if (typeof(Hub).IsAssignableFrom(serviceType)) 
                {
                    _container.Configure(c => c.AddType(serviceType, serviceType));
                    
                    return _container.TryGetInstance(serviceType);
                }
                else 
                {
                    return base.GetService(serviceType);
                }
            }
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.Model.HasImplementationsFor(serviceType))
                return _container.GetAllInstances(serviceType).Cast<object>();
            else
                return base.GetServices(serviceType);
        }
    }
}