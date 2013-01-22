using System;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public static class DomainEvents
    {
        [ThreadStatic] static List<Delegate> actions;

        public static Func<IEventStore> EventStore { get; set; }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            EventStore().Store(args);

            if (actions != null)
                foreach (var action in actions.OfType<Action<T>>())
                    action(args);
        }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }
    }
}