using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;
using NUnit.Framework;
using Should;
using StructureMap;
using System;
using www;

namespace nTestSwarmTests
{
    public class IntegrationTestBase : TestBase
    {
        protected IContainer StructureMap { get; set; }

        [TestFixtureSetUp]
        public void BaseTestFixtureSetUp()
        {
            StructureMap = IoC.Initialize();
            DeleteAllData();
            TestFixtureSetUp();
        }

        protected virtual void TestFixtureSetUp()
        {
            
        }

        [SetUp]
        public void BaseSetUp()
        {
            SetUp();
        }

        void DeleteAllData()
        {
            WithDbContext(context =>
            {
                context.Database.ExecuteSqlCommand("delete from events");
                context.Database.ExecuteSqlCommand("delete from RunUserAgentCompareResults");
                context.Database.ExecuteSqlCommand("delete from users");
                context.Database.ExecuteSqlCommand("delete from runuseragents");
                context.Database.ExecuteSqlCommand("delete from clientruns");
                context.Database.ExecuteSqlCommand("delete from clients");
                context.Database.ExecuteSqlCommand("delete from useragents");
                context.Database.ExecuteSqlCommand("delete from runs");
                context.Database.ExecuteSqlCommand("delete from jobs");
            });
        }

        protected virtual void SetUp()
        {
        }

        protected T GetInstance<T>()
        {
            return StructureMap.GetInstance<T>();
        }

        protected object GetInstance(Type type)
        {
            return StructureMap.GetInstance(type);
        }

        public nTestSwarmContext DbContext()
        {
            return new nTestSwarmContext("nTestSwarmContext.sdf");
        }

        public void WithDbContext(Action<nTestSwarmContext> action, ISystemTime time = null)
        {
            using (var context = DbContext())
            {
                if (action != null)
                {
                    StructureMap.Inject(typeof(IUserAgentCache), new UserAgentCache(() => context));
                    StructureMap.Inject(typeof(IDataBase), context);
                    StructureMap.Inject(typeof(nTestSwarmContext), context);
                    if (time != null) StructureMap.Inject(time);
                    action(context);
                    ObjectFactory.ResetDefaults();
                }
            }
        }
        public void Compare<T>(T actual, T expected) where T : class
        {
            actual.ShouldNotBeNull();
            expected.ShouldNotBeNull();
            var compare = new KellermanSoftware.CompareNetObjects.CompareObjects();
            compare.Compare(actual, expected);

        }
        public void Save(params Entity[] items)
        {
            WithDbContext(context =>
                              {
                                  context.AddMany(items);
                                  context.SaveChanges();
                              });
        }

        public ISystemTime SetTime(DateTime time)
        {
            SystemTime.NowThunk = () => time;
            return new SystemTime();
        }
    }
}