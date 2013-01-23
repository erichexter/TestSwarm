using System;
using System.Web.Script.Serialization;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public interface IEventSerializer
    {
        string Serialize<T>(T @event) where T : IDomainEvent;
        object Deserialize(string serializedEvent, Type type);
    }

    public class EventSerializer : IEventSerializer
    {
        static readonly JavaScriptSerializer _json = new JavaScriptSerializer();
        
        public string Serialize<T>(T @event) where T : IDomainEvent
        {
            return _json.Serialize(@event);
        }

        public object Deserialize(string serializedEvent, Type type)
        {
            return _json.Deserialize(serializedEvent, type);
        }
    }
}