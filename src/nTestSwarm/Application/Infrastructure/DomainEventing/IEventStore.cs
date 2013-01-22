using System;
using System.Collections.Generic;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public interface IEventStream : IDisposable
    {
        IEnumerable<IDomainEvent> All();
        void MarkAsProcessed(IDomainEvent runTimeEvent);
    }

    public interface IEventStore
    {
        void Store<T>(T @event) where T : IDomainEvent;
        IEventStream GetNotProcessedStream();
    }

    public class EventStore : IEventStore
    {
        readonly IDataBase _db;
        readonly IEventSerializer _serializer;

        public EventStore(IEventSerializer serializer, IDataBase db)
        {
            _serializer = serializer;
            _db = db;
        }

        public void Store<T>(T @event) where T : IDomainEvent
        {
            var persistentEvent = new Event
            {
                Type = typeof (T).AssemblyQualifiedName,
                SerializedData = _serializer.Serialize(@event)
            };

            _db.Add(persistentEvent);
            _db.SaveChanges();
        }

        public IEventStream GetNotProcessedStream()
        {
            var allNotProcessed = from e in _db.All<Event>()
                                  where e.Processed == null
                                  group e by e.Type
                                  into g
                                  select g;

            // don't do multiple type lookups
            var types = allNotProcessed.ToDictionary(@group => group.Key, @group => Type.GetType(group.Key, true));

            var typeKeyEventsValue = allNotProcessed.ToDictionary(events => types[events.Key], events => (IEnumerable<Event>) events);

            return new EventStream(e => _serializer.Deserialize(e.SerializedData, types[e.Type]), typeKeyEventsValue, this);
        }

        void MarkAsProcessed(Event persistentEvent)
        {
            persistentEvent.Processed = SystemTime.NowThunk();
            _db.SaveChanges();
        }

        class EventStream : IEventStream
        {
            readonly IDictionary<Type, IEnumerable<Event>> _allEvents;
            readonly Func<Event, object> _deserialization;
            readonly Dictionary<object, Event> _persistentToRuntimeEventMap = new Dictionary<object, Event>();
            readonly EventStore _store;

            public EventStream(Func<Event, object> deserialization,
                               IDictionary<Type, IEnumerable<Event>> allEvents,
                               EventStore store)
            {
                _deserialization = deserialization;
                _allEvents = allEvents;
                _store = store;
            }

            public void Dispose()
            {
            }

            public IEnumerable<IDomainEvent> All()
            {
                return _allEvents.SelectMany(x => x.Value).Select(getRunTimeEvent).Cast<IDomainEvent>();
            }

            public void MarkAsProcessed(IDomainEvent runTimeEvent)
            {
                var persistentEvent = _persistentToRuntimeEventMap[runTimeEvent];
                _store.MarkAsProcessed(persistentEvent);
            }

            object getRunTimeEvent(Event persistentEvent)
            {
                var runTimeEvent = _deserialization(persistentEvent);
                _persistentToRuntimeEventMap.Add(runTimeEvent, persistentEvent);
                return runTimeEvent;
            }
        }
    }
}