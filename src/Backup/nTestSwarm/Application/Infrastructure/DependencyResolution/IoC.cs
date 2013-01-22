using System;
using System.Web;
using StructureMap;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.NextRun;
using nTestSwarm.Application.Queries.NextRun;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;
using nTestSwarm.Controllers;

namespace nTestSwarm.Application.Infrastructure.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                        {
                            scan.AssemblyContainingType<HomeController>();
                            scan.WithDefaultConventions();
                            scan.ConnectImplementationsToTypesClosing(typeof (IHandler<,>));
                            scan.ConnectImplementationsToTypesClosing(typeof (IHandler<>));
                        });

                x.For(typeof (IRepository<>)).Use(typeof (Repository<>));
                
                x.For<nTestSwarmContext>().HybridHttpOrThreadLocalScoped()
                    .Use(() => new nTestSwarmContext());

                x.Forward<nTestSwarmContext, IDataBase>();
                
                x.For<IUserAgentCache>().Singleton().Use<UserAgentCache>();

                x.For<HttpContextBase>().Use(() => HttpContext.Current == null ? null : new HttpContextWrapper(HttpContext.Current));

                x.For<INextRunCache>().Singleton().Use(c =>
                                               {
                                                   var singleton = new NextRunCachePrimingSingleton(
                                                           c.GetInstance<IUserAgentCache>(),
                                                           c.GetInstance<Func<nTestSwarmContext>>());

                                                   return singleton.NewCache();
                                               });

                x.For<IRunQueue>().Use<CachingRunQueue>();
            });

            DomainEvents.EventStore = ObjectFactory.GetInstance<IEventStore>;

            return ObjectFactory.Container;
        }
    }
}