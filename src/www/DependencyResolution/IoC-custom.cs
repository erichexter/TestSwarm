using nTestSwarm.Application.Data;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Queries.NextRun;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;
using StructureMap;
using System;
using System.Web;

namespace nTestSwarm
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IHandler<,>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IHandler<>));
                    scan.RegisterConcreteTypesAgainstTheFirstInterface();
                });

                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                x.For<nTestSwarmContext>().Use(() => new nTestSwarmContext());
                x.Forward<nTestSwarmContext, IDataBase>();

                x.ForSingletonOf<IUserAgentCache>().Use<UserAgentCache>();

                x.For<HttpContextBase>().Use(() => HttpContext.Current == null ? null : new HttpContextWrapper(HttpContext.Current));

                x.ForSingletonOf<INextRunCache>().Use(c =>
                                               {
                                                   var singleton = new NextRunCachePrimingSingleton(
                                                           c.GetInstance<IUserAgentCache>(),
                                                           c.GetInstance<Func<nTestSwarmContext>>());

                                                   return singleton.NewCache();
                                               });

            });

            DomainEvents.EventStore = ObjectFactory.GetInstance<IEventStore>;

            return ObjectFactory.Container;
        }
    }
}
