using System;
using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarmTests.Application.Infrastructure.Events
{
    public class TestEvent : IDomainEvent
    {
        public TestEvent()
        {
        }

        public TestEvent(int number, string name)
        {
            Number = number;
            Name = name;
        }

        public int Number { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class EventStoreTests : IntegrationTestBase
    {
        readonly DateTime now = new DateTime(2001, 1, 1);

        protected override void TestFixtureSetUp()
        {
            SetTime(now);
            WithDbContext(context => DomainEvents.Raise(new TestEvent(1, "foo")));
        }

        [Test]
        public void Should_store_event()
        {
            var dbSet = DbContext().Events.ToArray();
            dbSet.Count().ShouldEqual(1);
            var @event = dbSet.Single();
            @event.Created.ShouldEqual(now);
            @event.SerializedData.ShouldEqual("{\"Number\":1,\"Name\":\"foo\"}");
            @event.Processed.ShouldBeNull();
            @event.Type.ShouldEqual(typeof (TestEvent).AssemblyQualifiedName);
        }
    }

    [TestFixture]
    public class EventStoreTests_should_process_events : IntegrationTestBase
    {
        readonly DateTime now = new DateTime(2001, 1, 1);

        protected override void TestFixtureSetUp()
        {
            SetTime(now);
            WithDbContext(context => DomainEvents.Raise(new TestEvent(1, "foo")));
            
            WithDbContext(context =>
            {
                var eventStore = GetInstance<IEventStore>();

                using (var eventStream = eventStore.GetNotProcessedStream())
                {
                    foreach (var runTimeEvent in eventStream.All())
                    {
                        eventStream.MarkAsProcessed(runTimeEvent);
                    }
                }
            });
        }

        [Test]
        public void Should_mark_processed_events()
        {
            WithDbContext(context =>
            {
                var eventStore = GetInstance<IEventStore>();
                eventStore.GetNotProcessedStream().All().ShouldBeEmpty();
            });
        }
    }
}