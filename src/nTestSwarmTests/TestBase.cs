using System;
using NUnit.Framework;
using Rhino.Mocks;
using nTestSwarm;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Services;

namespace nTestSwarmTests
{
    public abstract class TestBase
    {
        protected T S<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        [TearDown]
        public void BaseTearDown()
        {
            DomainEvents.ClearCallbacks();
            SystemTime.NowThunk = () => DateTime.Now;
            TearDown();
        }

        protected virtual void TearDown()
        {
        }
    }
}